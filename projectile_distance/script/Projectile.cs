using Godot;
using System;
using static MedAreaDetection;
using Utils;

public partial class Projectile : Node2D, ICiblable
{
    [ExportGroup("External")]
    [Export]
    public Node2D Cible
    {
        get => Poursuite.GetCible();
        set { Poursuite.SetCible(value); }
    }

    [ExportGroup("Internal")]
    [Export]
    Poursuite Poursuite;

    public override void _Ready() {
        base._Ready();

        Color color = Modulate;
        color.A = 0.0f;
        Modulate = color;

        Tween tween = CreateTween();
        
        tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Circ);

        tween.TweenProperty(this, "modulate:a", 1.0f, 0.3f);
        tween.SetParallel();
        tween.TweenProperty(this, "scale:y", 1.0f, 0.3f).From(1.5f);

    }

    public void SetCible(Node2D InCible)
    {
        Cible = (Node2D)InCible;
    }

    public void Colide(Node2D InEntered)
    {
        DcmProjectile parent = (DcmProjectile)GetParent();
        parent.EnsureValid().Colide(this, InEntered);
    }
}
