using System;
using System.Collections.Generic;
using Godot;
using Utils;
using static MedAttack;

public partial class DcmObject : Node2D, ISpawn, ICollide
{
    [ExportGroup("External")]
    [Export]
    private PackedScene TreasureScene,
        GemScene,
        TrapScene;

    [Export]
    private MedAttack MediateurAttack;

    [Export]
    private MedCible MediateurCible;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer1,
        timer2;

    private List<Timer> TimerList = [];
    private List<PackedScene> SceneList = [];

    public void Collide(
        EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        MediateurAttack.Collide(InAlgoSelectionDetection, InEntering, InEntered);
    }

    public override void _Ready()
    {
        base._Ready();
        TimerList.AddRange(new[] { timer1, timer2 });
        SceneList.AddRange(new[] { TreasureScene, TrapScene });
        _ActivateObject();
    }

    private void _ActivateObject()
    {
        foreach (Timer timer in TimerList)
        {
            timer.EnsureValid().Timeout += () =>
            {
                int index = TimerList.IndexOf(timer);
                BaseObject spawn = (BaseObject)((ISpawn)this).Spawn(SceneList[index]);
                spawn.GlobalPosition += _DefinePosition();
                AddChild(spawn);
            };
        }
    }

    public void SpawnGem(Vector2 InPosition)
    {
        BaseObject spawn = (BaseObject)((ISpawn)this).Spawn(GemScene);
        spawn.GlobalPosition = InPosition;
        Callable
            .From(() =>
            {
                AddChild(spawn);
            })
            .CallDeferred();
    }

    private Vector2 _DefinePosition()
    {
        float tailleX = 1320.0f;
        float tailleY = 720.0f;
        float buffer = 20.0f;

        Vector2 playerPos = MediateurCible.GetPlayerPosition();
        Camera2D camera2D = MediateurCible.GetParent().GetNode<Camera2D>("Camera2D");
        Vector2 cameraSize = camera2D.GetViewportRect().Size / camera2D.Zoom;

        float cameraH = cameraSize.Y;

        float cameraW = cameraSize.X;

        float cameraTop = playerPos.Y - (cameraH / 2);
        float cameraBottom = playerPos.Y + (cameraH / 2);
        float cameraRight = playerPos.X + (cameraW / 2);
        float cameraLeft = playerPos.X - (cameraW / 2);

        int coteApparition = GD.RandRange(0, 3);
        Vector2 v = new(0, 0);

        switch (coteApparition)
        {
            case 0:
                if (cameraTop - buffer >= -tailleY / 2)
                {
                    v = new(
                        (float)GD.RandRange(-tailleX / 2, tailleX / 2),
                        (float)GD.RandRange(-cameraH / 2 - buffer, -tailleY / 2)
                    );
                    break;
                }
                goto case 2;
            case 1:
                if (cameraRight + buffer <= tailleX / 2)
                {
                    v = new(
                        (float)GD.RandRange(cameraW / 2 + buffer, tailleX / 2),
                        (float)GD.RandRange(tailleY / 2, -tailleY / 2)
                    );
                    break;
                }
                goto case 3;
            case 2:
                if (cameraBottom + buffer <= tailleY / 2)
                {
                    v = new(
                        (float)GD.RandRange(-tailleX / 2, tailleX / 2),
                        (float)GD.RandRange(cameraH / 2 + buffer, tailleY / 2)
                    );
                    break;
                }
                goto case 0;
            case 3:
                if (cameraLeft - buffer >= -tailleX / 2)
                {
                    v = new(
                        (float)GD.RandRange(-cameraW / 2 + buffer, -tailleX / 2),
                        (float)GD.RandRange(tailleY / 2, -tailleY / 2)
                    );
                    break;
                }
                goto case 1;
        }
        return v;
    }
}
