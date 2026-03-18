using System;
using Godot;

public partial class Gem : BaseObject, ICiblable
{
    [ExportGroup("Internal")]
    [Export]
    private Poursuite Poursuite;
    private float velocity = 20.0f;
    private float acc = 5.0f;

    public override void _Ready()
    {
        base._Ready();
        Poursuite.Velocity = velocity;
        base.XpValue = 1;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Poursuite.GetCible() != null)
        {
            Poursuite.Velocity += acc;
        }
    }

    public override void Animate(Player InPlayer)
    {
        SetCible(InPlayer);
    }

    public void SetCible(Node2D InCible)
    {
        Poursuite.SetCible(InCible);
    }
}
