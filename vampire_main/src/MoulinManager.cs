using System;
using Godot;

public partial class MoulinManager : Node
{
    private double time = 0.0;

    [Export]
    public float frequency = 2.0f;

    [Export]
    public float minSpeed = 1.0f;

    [Export]
    public float maxSpeed = 2.0f;

    public override void _Ready()
    {
        foreach (AnimatedSprite2D moulin in this.GetChildren())
        {
            moulin.SpeedScale = 0.1f;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        time += delta;

        float amplitude = (maxSpeed - minSpeed) / 2.0f;
        float midpoint = (maxSpeed + minSpeed) / 2.0f;

        float sinusValue = Mathf.Sin((float)time * frequency);
        float newSpeed = (sinusValue * amplitude) + midpoint;

        // GD.Print(newSpeed);

        foreach (AnimatedSprite2D moulin in this.GetChildren())
        {
            moulin.SpeedScale = newSpeed;
        }
    }
}
