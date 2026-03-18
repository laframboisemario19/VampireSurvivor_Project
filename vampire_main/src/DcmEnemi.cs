using System;
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
    private PackedScene SpawneeScene;

    [Export]
    private Vector2 IntervalRange = new(1.0f, 2.0f);

    [Export]
    private MedCible MediateurCible;

    [Export]
    MedCible.EAlgoSelectionCible AlgoSelectionCible;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer;

    private float _tailleX = 700.0f;
    private float _tailleY = 500.0f;

    // private float _decalageX = 900.0f;
    // private float _decalageY = 500.0f;
    private float _buffer = 10.0f;

    public override void _Ready()
    {
        base._Ready();
        timer.EnsureValid().Timeout += () =>
        {
            Camera2D _camera2D = MediateurCible.GetParent().GetNode<Camera2D>("Camera2D");

            var spawn = (Node2D)((ISpawn)this).Spawn(SpawneeScene);

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
                            (float)GD.RandRange(_tailleY / 2, -_tailleY / 2)
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
                            (float)GD.RandRange(_tailleY / 2, -_tailleY / 2)
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
        };
    }

    public IEnumerable<Node2D> GatherChildren()
    {
        return ChildManipulator.GatherChildren(SpawneeScene, this);
    }
}
