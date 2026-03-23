using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Godot;
using Godot.Collections;
using Utils;
using Vector2 = Godot.Vector2;

public partial class DcmEnemi : Node2D, ISpawn
{
    [ExportGroup("External")]
    [Export]
    private PackedScene SpawneeScene,
        ZombieBase,
        ZombieRapide,
        ZombieTank,
        ZombieGene,
        ZombieBoss;

    [Export]
    private Vector2 IntervalRange = new(1.0f, 2.0f);

    [Export]
    private MedCible MediateurCible;

    [Export]
    MedCible.EAlgoSelectionCible AlgoSelectionCible;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer,
        timer1,
        timer2,
        timer3,
        timer4;

    int i = 1;

    private ArrayList timerList = [];
    private ArrayList sceneList = [];

    public enum ETypeEnemi
    {
        eZombieBase = 0,
        eZombieRapide = 1,
        eZombieTank = 2,
        eZombieGene = 3,
        eZombieBoss = 4,
    }

    private float _tailleX = 1350.0f;
    private float _tailleY = 750.0f;
    private float _buffer = 10.0f;

    public override void _Ready()
    {
        base._Ready();
        timerList.AddRange(new[] { timer1, timer2, timer3, timer4 });
        sceneList.AddRange(new[] { ZombieBase, ZombieRapide, ZombieTank, ZombieGene, ZombieBoss });

        timer.EnsureValid().Timeout += () =>
        {
            if (i < 4)
            {
                ActivateWave((ETypeEnemi)i);
                i++;
                GD.Print(i);
            }
            else if (i == 4)
            {
                _SpawnWithTarget(ETypeEnemi.eZombieBoss);
                i++;
            }
        };
    }

    public void ActivateWave(ETypeEnemi InEnemiType)
    {
        Timer timer = (Timer)timerList[(int)InEnemiType];
        timer.EnsureValid().Timeout += () =>
        {
            _SpawnWithTarget(InEnemiType);
        };
    }

    private void _SpawnWithTarget(ETypeEnemi InEnemiType)
    {
        Camera2D _camera2D = MediateurCible.GetParent().GetNode<Camera2D>("Camera2D");

        var spawn = (Node2D)((ISpawn)this).Spawn((PackedScene)sceneList[(int)InEnemiType]);

        Vector2 playerPos = MediateurCible.GetPlayerPosition();

        Vector2 cameraSize = _camera2D.GetViewportRect().Size / _camera2D.Zoom;

        float _cameraH = cameraSize.Y;

        float _cameraW = cameraSize.X;

        float cameraTop = playerPos.Y - (_cameraH / 2);
        float cameraBottom = playerPos.Y + (_cameraH / 2);
        float cameraRight = playerPos.X + (_cameraW / 2);
        float cameraLeft = playerPos.X - (_cameraW / 2);

        int coteApparition = GD.RandRange(0, 3);
        Vector2 v = new(0, 0);

        switch (coteApparition)
        {
            case 0:
                if (cameraTop - _buffer >= -_tailleY / 2)
                {
                    v = new(
                        (float)GD.RandRange(-_tailleX / 2, _tailleX / 2),
                        (float)GD.RandRange(-_cameraH / 2 - _buffer, -_tailleY / 2)
                    );
                    break;
                }
                goto case 2;
            case 1:
                if (cameraRight + _buffer <= _tailleX / 2)
                {
                    v = new(
                        (float)GD.RandRange(_cameraW / 2 + _buffer, _tailleX / 2),
                        (float)GD.RandRange(-_tailleY / 2, _tailleY / 2)
                    );
                    break;
                }
                goto case 3;
            case 2:
                if (cameraBottom + _buffer <= _tailleY / 2)
                {
                    v = new(
                        (float)GD.RandRange(-_tailleX / 2, _tailleX / 2),
                        (float)GD.RandRange(_cameraH / 2 + _buffer, _tailleY / 2)
                    );
                    break;
                }
                goto case 0;
            case 3:
                if (cameraLeft - _buffer >= -_tailleX / 2)
                {
                    v = new(
                        (float)GD.RandRange(-_cameraW / 2 + _buffer, -_tailleX / 2),
                        (float)GD.RandRange(-_tailleY / 2, _tailleY / 2)
                    );
                    break;
                }
                goto case 1;
        }

        //spawn.GlobalPosition = MediateurCible.GetPlayerPosition() + v;
        spawn.GlobalPosition += v;

        Node2D cible = MediateurCible
            .EnsureValid()
            .ChoisirCible(AlgoSelectionCible, GlobalPosition);
        if (spawn is ICiblable ciblable)
        {
            ciblable.SetCible(cible);
        }
        AddChild(spawn);
    }

    public IEnumerable<Node2D> GatherChildren()
    {
        return ChildManipulator.GatherChildren(SpawneeScene, this);
    }

    public void SpawnRandomEnemy()
    {
        Camera2D _camera2D = MediateurCible.GetParent().GetNode<Camera2D>("Camera2D");
        Vector2 cameraSize = _camera2D.GetViewportRect().Size / _camera2D.Zoom;
        float _cameraH = cameraSize.Y;
        float _cameraW = cameraSize.X;

        int index = (int)Random.Shared.NextInt64(0, 4);
        var spawn = (Node2D)((ISpawn)this).Spawn((PackedScene)sceneList[index]);
        Vector2 playerPos = MediateurCible.GetPlayerPosition();

        float posX = 0;
        float posY = 0;

        while (
            posX + playerPos.X <= -_tailleX / 2
            || posX + playerPos.X >= _tailleX / 2
            || posY + playerPos.Y <= -_tailleY / 2
            || posY + playerPos.Y >= _tailleY / 2
            || posY == 0
            || posX == 0
        )
        {
            float minOffset = 20;
            float multX = Random.Shared.Next(1, 3) == 1 ? -1 : 1;
            float multY = Random.Shared.Next(1, 3) == 1 ? -1 : 1;

            posX = (float)GD.RandRange(minOffset, _cameraW / 2) * multX;
            posY = (float)GD.RandRange(minOffset, _cameraH / 2) * multY;
        }

        //GD.Print($"E: {posX}, {posY}");
        GD.Print($"Player: {playerPos.X}, {playerPos.Y}");
        spawn.GlobalPosition = playerPos + new Vector2(posX, posY);
        GD.Print($"E: {spawn.GlobalPosition.X}, {spawn.GlobalPosition.Y}");

        Node2D cible = MediateurCible
            .EnsureValid()
            .ChoisirCible(AlgoSelectionCible, GlobalPosition);
        if (spawn is ICiblable ciblable)
        {
            ciblable.SetCible(cible);
        }
        AddChild(spawn);
    }

    public void StartGame()
    {
        ActivateWave((ETypeEnemi)0);
        timer.Start();
    }

    public void GameOver()
    {
        foreach (Timer t in timerList)
        {
            t.Stop();
        }
        timer.Stop();
    }
}
