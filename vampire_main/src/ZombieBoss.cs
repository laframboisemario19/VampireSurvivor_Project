using System;
using Godot;

public partial class ZombieBoss : BaseEnemy
{
    [ExportGroup("Intern")]
    [Export]
    private Timer timer;
    private DcmEnemi dcmEnemi;

    public override void _Ready()
    {
        base._Ready();
        dcmEnemi = (DcmEnemi)GetParent();
        timer.Timeout += dcmEnemi.SpawnRandomEnemy;
    }
}
