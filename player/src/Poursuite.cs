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
    private float velocity = 100.0f;
    public float Velocity
    {
        get { return velocity; }
        set
        {
            if (value >= 0 && value <= 1000)
            {
                this.velocity = value;
            }
        }
    }

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

            if (Poursuivant is Projectile)
            {
                float angle = (float)Math.Atan2(deplacement.Y, deplacement.X);
                Poursuivant.Rotation = angle;
            }
            else
            {
                float scaleX =
                    deplacement.X > 0.0f
                        ? Math.Abs(Poursuivant.Scale.X)
                        : -Math.Abs(Poursuivant.Scale.X);
                Vector2 newScale = new Vector2(scaleX, Poursuivant.Scale.Y);
                Poursuivant.Scale = newScale;
            }
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
