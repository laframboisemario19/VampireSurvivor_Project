using Godot;
using Utils;
using static MedCible;
using static MedAttack;
using System.Linq;
using System.Collections;
using System.Numerics;
using System;

public partial class DcmProjectile : Node2D, ISpawn
{
    [ExportGroup("External")]
    [Export]
    private PackedScene ProjectileLinear, ProjectileCircle;

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
    private Timer timer1, timer2, timer3, timer4, timer5, timer6;

    private ArrayList timerList = [];
    private ArrayList sceneList = [];

    private float spiralAngle;

    public enum EProjectileType
    {
        eLinearTarget = 0,
        eCircleTarget = 1,
        eLinearSpiral = 2,
        eCircleSpiral = 3,
        eLinearExplosion = 4,
        eCircleExplosion = 5

    }

    public override void _Ready()
    {
        base._Ready();
        timerList.AddRange(new[] {timer1, timer2, timer3, timer4, timer5, timer6});
        sceneList.AddRange(new[] {ProjectileLinear, ProjectileCircle});
    }

    public void Collide(Node2D InEntering, Node2D InEntered)
    {
        MediateurAttack.Collide(InAlgoSelectionDetection, InEntering, InEntered);
    }

    public void ActivateProjectile(EProjectileType InProjectileType)
    {
        Timer timer = (Timer)timerList[(int)InProjectileType];
        timer.EnsureValid().Timeout += () =>
        {

            if(InProjectileType == EProjectileType.eLinearTarget || InProjectileType == EProjectileType.eCircleTarget)
            {
                _SpawnCible(InProjectileType);
            }
            else if(InProjectileType == EProjectileType.eLinearExplosion || InProjectileType == EProjectileType.eCircleExplosion)
            {
                _SpawnExplosion(InProjectileType);         
            }
            else
            {
                _SpawnSpiral(InProjectileType);
            }
            
        };

    }

    private void _SpawnCible(EProjectileType InProjectileType)
    {
        Projectile spawn = (Projectile)((ISpawn)this).Spawn((PackedScene)sceneList[(int)InProjectileType % 2] );
        Node2D cible = MediateurCible.EnsureValid().ChoisirCible(InAlgoSelectionCible, GlobalPosition);
            if (spawn is ICiblable ciblable)
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

    private void _SpawnExplosion(EProjectileType InProjectileType)
    {
        int nbSpawn = 12;
        float length = 250;
        float angle = 0;
        for(int i = 0; i < nbSpawn * 2; i += 2)
        {
            Projectile spawn = (Projectile)((ISpawn)this).Spawn((PackedScene)sceneList[(int)InProjectileType % 2] );

            spawn.GlobalPosition = MediateurCible.GetPlayerPosition();
            AddChild(spawn);
            spawn.MovementType = InProjectileType;

            Tween tw = spawn.CreateTween();
            angle = i * Mathf.Pi / nbSpawn;
            Godot.Vector2 movement = new(Mathf.Cos(angle), Mathf.Sin(angle));
            movement *= length;
            tw.TweenProperty(spawn, "position", spawn.Position + movement, 5.0);
            tw.TweenCallback(Callable.From(() => {spawn.Die();}));
            
            
            float face = (float)Mathf.Atan2(Mathf.Sin(angle), Mathf.Cos(angle));
            spawn.Rotation = face;
            
            if ((int)InProjectileType % 2 == 1)
            {
                spawn.InfiniteTurn();
            }
        } 
    }

    private void _SpawnSpiral(EProjectileType InProjectileType)
    {
        int nbAngle = 24;
        float length = 250;

        Projectile spawn = (Projectile)((ISpawn)this).Spawn((PackedScene)sceneList[(int)InProjectileType % 2] );

        spawn.GlobalPosition = MediateurCible.GetPlayerPosition();
        AddChild(spawn);
        spawn.MovementType = InProjectileType;

        Tween tw = spawn.CreateTween();
        float angle = spiralAngle * Mathf.Pi / nbAngle;
        Godot.Vector2 movement = new(Mathf.Cos(angle), Mathf.Sin(angle));
        movement *= length;
        tw.TweenProperty(spawn, "position", spawn.Position + movement, 5.0);
        tw.TweenCallback(Callable.From(() => {spawn.Die();}));
        
        
        float face = (float)Mathf.Atan2(Mathf.Sin(angle), Mathf.Cos(angle));
        spawn.Rotation = face;
        
        if ((int)InProjectileType % 2 == 1)
        {
            spawn.InfiniteTurn();
        }
        spiralAngle += 2;
    }
}
