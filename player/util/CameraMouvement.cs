using Godot;
using System;

public partial class CameraMouvement : Camera2D
{
    [ExportGroup("General")]
    [Export] public float smoothingSpeed = 10.0f;
    [ExportGroup("External")]
    [Export] Node2D playerToFollow;
    public override void _Ready()
    {
        if (playerToFollow is null)
        {
            return;
        }
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        Vector2 playerPosition = playerToFollow.GlobalPosition;
        Vector2 cameraPosition = GlobalPosition.Lerp(playerPosition, (float)(smoothingSpeed * delta));
        
        this.GlobalPosition = cameraPosition;
    }
}