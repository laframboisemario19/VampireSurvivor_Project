using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Utils;
using static MedAttack;
using static MedCible;

public partial class DcmProjectile : Node2D, ISpawn
{
    [ExportGroup("External")]
    [Export]
    private PackedScene ProjectileLinear,
        ProjectileCircle;

    [Export]
    private MedCible MediateurCible;

    [Export]
    EAlgoSelectionCible InAlgoSelectionCible;

    [Export]
    private MedAttack MediateurAttack;

    [Export]
    EAlgoSelectionDetection InAlgoSelectionDetection;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer1,
        timer2,
        timer3,
        timer4,
        timer5,
        timer6;

    private List<Timer> timerList = [];
    private List<PackedScene> sceneList = [];

    private float spiralAngle;

    public enum EProjectileType
    {
        eLinearTarget = 0,
        eCircleTarget = 1,
        eLinearSpiral = 2,
        eCircleSpiral = 3,
        eLinearExplosion = 4,
        eCircleExplosion = 5,
    }

    public override void _Ready()
    {
        base._Ready();
        timerList.AddRange(new[] { timer1, timer2, timer3, timer4, timer5, timer6 });
        sceneList.AddRange(new[] { ProjectileLinear, ProjectileCircle });
    }

    public void Collide(Node2D InEntering, Node2D InEntered)
    {
        MediateurAttack.Collide(InAlgoSelectionDetection, InEntering, InEntered);
    }

    public void ActivateProjectile(EProjectileType InProjectileType)
    {
        Timer timer = timerList[(int)InProjectileType];
        timer.EnsureValid().Timeout += () =>
        {
            if (
                InProjectileType == EProjectileType.eLinearTarget
                || InProjectileType == EProjectileType.eCircleTarget
            )
            {
                _SpawnWithTarget(InProjectileType);
            }
            else if (
                InProjectileType == EProjectileType.eLinearExplosion
                || InProjectileType == EProjectileType.eCircleExplosion
            )
            {
                int nbSpawn = 12;

                for (int i = 0; i < nbSpawn; i++)
                {
                    float angle = i * 2 * Mathf.Pi / nbSpawn;
                    _SpawnWithoutTarget(InProjectileType, 250, angle);
                }
            }
            else
            {
                int nbAngle = 24;
                float angle = spiralAngle * Mathf.Pi / nbAngle;
                _SpawnWithoutTarget(InProjectileType, 250, angle);
                spiralAngle += 2;
            }
        };
    }

    private void _SpawnWithTarget(EProjectileType InProjectileType)
    {
        Projectile spawn = (Projectile)
            ((ISpawn)this).Spawn(sceneList[(int)InProjectileType % 2]);
        Node2D cible = MediateurCible
            .EnsureValid()
            .ChoisirCible(InAlgoSelectionCible, MediateurCible.GetPlayerPosition());
        if (spawn is ICiblable)
        {
            spawn.SetCible(cible);
            spawn.MovementType = InProjectileType;
        }

        spawn.GlobalPosition = MediateurCible.GetPlayerPosition();
        AddChild(spawn);

        if ((int)InProjectileType % 2 == 1)
        {
            spawn.InfiniteTurn();
        }
    }

    private void _SpawnWithoutTarget(
        EProjectileType InProjectileType,
        float InLength,
        float InAngle
    )
    {
        Projectile spawn = (Projectile)
            ((ISpawn)this).Spawn(sceneList[(int)InProjectileType % 2]);

        spawn.GlobalPosition = MediateurCible.GetPlayerPosition();
        AddChild(spawn);
        spawn.MovementType = InProjectileType;

        Tween tw = spawn.CreateTween();
        Godot.Vector2 movement = new(Mathf.Cos(InAngle), Mathf.Sin(InAngle));
        movement *= InLength;
        tw.TweenProperty(spawn, "position", spawn.Position + movement, 5.0);
        tw.TweenCallback(
            Callable.From(() =>
            {
                spawn.Die();
            })
        );

        float face = (float)Mathf.Atan2(Mathf.Sin(InAngle), Mathf.Cos(InAngle));
        spawn.Rotation = face;

        if ((int)InProjectileType % 2 == 1)
        {
            spawn.InfiniteTurn();
        }
    }

    public void GameOver()
    {
        foreach (Timer timer in timerList)
        {
            timer.Stop();
        }
    }
}
