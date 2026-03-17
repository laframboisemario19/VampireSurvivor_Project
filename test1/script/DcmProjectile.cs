using Godot;
using System;
using Utils;

public partial class DcmProjectile : Node2D, ISpawn
{
    [ExportGroup("External")]
    [Export]
    private PackedScene SpawneeScene;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer;

    public override void _Ready()
    {
        base._Ready();
        timer.EnsureValid().Timeout += () => {
            AddChild(((ISpawn)this).Spawn(SpawneeScene));
        };
        
    }
}
