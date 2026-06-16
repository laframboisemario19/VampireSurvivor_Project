using System;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class WindManager : Node
{
    private readonly RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        base._Ready();

        _rng.Randomize();

        foreach (Sprite2D nuage in this.GetChildren())
        {
            float randomOpacity = _rng.RandfRange(0.4f, 0.6f);
            Color newOpacity = nuage.Modulate;
            newOpacity.A = randomOpacity;
            nuage.Modulate = newOpacity;

            float randomXPosition = _rng.RandfRange(-100.0f, 100.0f);
            Vector2 newPositionX = nuage.GlobalPosition;
            newPositionX.X += randomXPosition;
            nuage.GlobalPosition = newPositionX;

            float randomYPosition = _rng.RandfRange(-100.0f, 100.0f);
            Vector2 newPositionY = nuage.GlobalPosition;
            newPositionY.Y += randomYPosition;
            nuage.GlobalPosition = newPositionY;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        foreach (Sprite2D nuage in this.GetChildren())
        {
            Vector2 nuagePosition = nuage.GlobalPosition;

            float randomSpeed = _rng.RandfRange(30.0f, 50.0f);

            nuagePosition.X += randomSpeed * (float)delta;

            if (nuagePosition.X > 1700)
            {
                nuagePosition.X = -1700;
            }

            nuage.GlobalPosition = nuagePosition;
        }
    }
}
