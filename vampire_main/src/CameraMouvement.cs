using System;
using Godot;

public partial class CameraMouvement : Camera2D
{
    [ExportGroup("General")]
    [Export]
    public float smoothingSpeed = 10.0f;

    [ExportGroup("External")]
    [Export]
    Node2D playerToFollow;

    public override void _Ready()
    {
        if (playerToFollow is null)
        {
            return;
        }

        Vector2I CenterPosition = new(
            (int)GetScreenCenterPosition().X,
            (int)GetScreenCenterPosition().Y
        );

        Vector2I targetSize = new((int)(1920.0 * 1.5), (int)(1080.0 * 1.5));
        GetTree().Root.ContentScaleSize = targetSize;
        GetTree().Root.ContentScaleMode = Window.ContentScaleModeEnum.CanvasItems;
        GetTree().Root.ContentScaleAspect = Window.ContentScaleAspectEnum.Keep;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 playerPosition = playerToFollow.GlobalPosition;
        Vector2 cameraPosition = GlobalPosition.Lerp(
            playerPosition,
            (float)(smoothingSpeed * delta)
        );

        this.GlobalPosition = cameraPosition;
    }
}
