using System;
using Godot;
using Utils;

public partial class Zombie : BaseEnemy, ICiblable
{
    [ExportGroup("External")]
    [Export]
    public Node2D Cible
    {
        get => base.Poursuite.EnsureValid().GetCible();
        set { base.Poursuite.EnsureValid().SetCible(value); }
    }

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
}
