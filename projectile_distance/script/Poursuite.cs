using System;
using System.Linq;
using Godot;
using Utils;

public partial class Poursuite : Node2D
{
    private Node2D Cible;

    [Export]
    private Node2D Poursuivant;

    [Export(PropertyHint.Range, "0,1000,10,suffix:pps")]
    private float Velocity = 100.0f;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double InDelta)
    {
        base._PhysicsProcess(InDelta);
        if (Cible != null)
        {
            Cible.EnsureValid();
            Vector2 altDirection = Poursuivant.GlobalPosition.DirectionTo(Cible.GlobalPosition);

            Vector2 deplacement = altDirection * Velocity * (float)InDelta;
            Poursuivant.GlobalPosition += deplacement;

            float scaleX =
                deplacement.X > 0.0f
                    ? Math.Abs(Poursuivant.Scale.X)
                    : -Math.Abs(Poursuivant.Scale.X);
            Vector2 newScale = new Vector2(scaleX, Poursuivant.Scale.Y);
            Poursuivant.Scale = newScale;
            }
    }

    public void SetCible(Node2D InCible)
    {
        Cible = InCible;
    }

    public Node2D GetCible()
    {
        return this.Cible;
    }
}
