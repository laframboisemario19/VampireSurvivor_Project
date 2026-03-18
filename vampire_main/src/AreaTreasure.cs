using System;
using Godot;

public partial class AreaTreasure : Area2D
{
    [ExportGroup("Internal")]
    [Export]
    private AnimatedSprite2D InSprite;

    public void _on_area_exited(Area2D InArea)
    {
        InSprite.PlayBackwards("open");
    }
}
