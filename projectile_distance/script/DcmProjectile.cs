using Godot;
using System;
using Utils;
using static MedCible;

public partial class DcmProjectile : Node2D, ISpawn
{
    [ExportGroup("External")]
    [Export]
    private PackedScene SpawneeScene;

    [Export]
    private MedCible MediateurCible;

    [Export]
    EAlgoSelectionCible InAlgoSelectionCible;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer;

    public override void _Ready()
    {
        base._Ready();
        timer.EnsureValid().Timeout += () => {
            Projectile spawn = (Projectile)((ISpawn)this).Spawn(SpawneeScene);
            Node2D cible = MediateurCible.EnsureValid().ChoisirCible(InAlgoSelectionCible, GlobalPosition);
            if (spawn is ICiblable ciblable)
            {
                spawn.SetCible(cible);
            }
            AddChild(spawn);
        };
        
    }
}
