using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class WindManager : Node
{

    public override void _Ready() {
        base._Ready();
        
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        foreach (Sprite2D nuage in this.GetChildren())
        {
            float randomOpacity = rng.RandfRange(0.4f,0.6f);
            Color newOpacity = nuage.Modulate;
            newOpacity.A = randomOpacity;
            nuage.Modulate = newOpacity;

            float randomXPosition = rng.RandfRange(-100.0f,100.0f);
            Vector2 newPositionX = nuage.GlobalPosition;
            newPositionX.X += randomXPosition;
            nuage.GlobalPosition = newPositionX;

            float randomYPosition = rng.RandfRange(-100.0f,100.0f);
            Vector2 newPositionY = nuage.GlobalPosition;
            newPositionY.Y += randomYPosition;
            nuage.GlobalPosition = newPositionY;
        }
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        var rng = new RandomNumberGenerator();
        rng.Randomize();

        foreach (Sprite2D nuage in this.GetChildren())
        {
            Vector2 nuagePosition = nuage.GlobalPosition;

            float randomSpeed = rng.RandfRange(30.0f,50.0f);

            nuagePosition.X += randomSpeed * (float)delta;

            if (nuagePosition.X > 1700)
            {
                nuagePosition.X = -1700;
            }

            nuage.GlobalPosition = nuagePosition;
        }
    }   


}
