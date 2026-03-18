using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using Utils;

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

    public override void _Ready()
    {
        base._Ready();
        timer.EnsureValid().Timeout += () =>
        {
            Zombie spawn = (Zombie)((ISpawn)this).Spawn(SpawneeScene);
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
