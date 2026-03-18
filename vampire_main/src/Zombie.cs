using System;
using Godot;
using Utils;

public partial class Zombie : BaseEnemy, ICiblable
{
    public override void _Ready()
    {
        base._Ready();
        Vector2 oldScale = Scale;
        Scale = Vector2.Zero;
        Tween tween = CreateTween();
        tween
            .TweenProperty(this, "scale", oldScale, 0.5f)
            .SetTrans(Tween.TransitionType.Back)
            .SetEase(Tween.EaseType.Out);
    }
}
