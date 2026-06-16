using System;
using Godot;

public partial class FlockManager : Node
{
    private int dice;
    private int randomSlop;
    private readonly RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        base._Ready();

        _rng.Randomize();

        foreach (AnimatedSprite2D oiseau in this.GetChildren())
        {
            Vector2 newPositionX = oiseau.GlobalPosition;
            newPositionX.X += 800;
            oiseau.GlobalPosition = newPositionX;

            float randomYPosition = _rng.RandfRange(-300.0f, 300.0f);
            Vector2 newPositionY = oiseau.GlobalPosition;
            newPositionY.Y += randomYPosition;
            oiseau.GlobalPosition = newPositionY;

            dice = _rng.RandiRange(1, 2);
            randomSlop = _rng.RandiRange(5, 10);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        foreach (AnimatedSprite2D oiseau in this.GetChildren())
        {
            Vector2 oiseauPosition = oiseau.GlobalPosition;

            float randomSpeed = _rng.RandfRange(50.0f, 100.0f);

            oiseauPosition.X -= randomSpeed * (float)delta;
            if (dice == 1)
            {
                oiseauPosition.Y -= (randomSpeed / randomSlop) * (float)delta;
            }
            else
            {
                oiseauPosition.Y += (randomSpeed / randomSlop) * (float)delta;
            }

            if (oiseauPosition.Y < -450 || oiseauPosition.Y > 450 || oiseauPosition.X < -810)
            {
                float randomYPosition = _rng.RandfRange(-300.0f, 300.0f);
                oiseauPosition.Y = randomYPosition;
                oiseauPosition.X = 800;
                dice = _rng.RandiRange(1, 2);
                randomSlop = _rng.RandiRange(5, 10);
            }

            oiseau.GlobalPosition = oiseauPosition;
        }
    }
}
