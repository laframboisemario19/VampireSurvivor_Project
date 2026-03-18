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

    private Vector2 _offsetBounds = new(1920, 1080);
    private float _tailleX = 1000.0f;
    private float _tailleY = 600.0f;
    private float _decalageX = 900.0f;
    private float _decalageY = 500.0f;

    public override void _Ready()
    {
        base._Ready();
        timer.EnsureValid().Timeout += () =>
        {
            var spawn = (Node2D)((ISpawn)this).Spawn(SpawneeScene);

            int coteApparition = GD.RandRange(0, 3);
            Vector2 v = new(0, 0);

            switch (coteApparition)
            {
                case 0:
                    v = new((float)GD.RandRange(-_tailleX, _tailleX), -_decalageY);
                    break;
                case 1:
                    v = new(_decalageX, (float)GD.RandRange(-_decalageY, _decalageY));
                    break;
                case 2:
                    v = new((float)GD.RandRange(-_tailleX, _tailleX), _decalageY);
                    break;
                case 3:
                    v = new(_decalageX, (float)GD.RandRange(-_decalageY, _decalageY));
                    break;
            }

            spawn.GlobalPosition = GlobalPosition + v;

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
