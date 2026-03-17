using System;
using Godot;
using Utils;

public partial class Zombie : Node2D, ICiblable
{
    [ExportGroup("External")]
    [Export]
    public Node2D Cible
    {
        get => Poursuite.EnsureValid().GetCible();
        set { Poursuite.EnsureValid().SetCible(value); }
    }

    [ExportGroup("Internal")]
    [Export]
    Poursuite Poursuite;

    [Export]
    private AnimatedSprite2D AnimatedSprite;

    private bool _isDying = false;

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

    public void SetCible(Node2D InCible)
    {
        Cible = InCible;
    }

    public void Die()
    {
        if (!_isDying)
        {
            _isDying = true;
            Poursuite.Velocity = 60.0f;
            AnimatedSprite.Animation = "explode";
            AnimatedSprite.AnimationFinished += () => QueueFree();
        }
    }
}
