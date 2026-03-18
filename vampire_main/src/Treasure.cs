using System;
using Godot;

public partial class Treasure : BaseObject
{
    [ExportGroup("Internal")]
    [Export]
    private AnimatedSprite2D Sprite;

    public override void _Ready()
    {
        base._Ready();
        base.XpValue = 10;
    }

    public override void Animate(Player InPlayer)
    {
        Sprite.Play("open");
    }
}
