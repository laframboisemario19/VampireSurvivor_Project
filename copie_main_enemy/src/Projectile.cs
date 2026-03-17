using Godot;
using System;
using static MedAttack;
using static DcmProjectile;
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

    [Export]
    private AnimatedSprite2D AnimatedSprite;
    public EProjectileType MovementType
    { get; set; }

    private Tween tw;
    private bool _isDying = false;

    public override void _Ready() {
        base._Ready();

        Color color = Modulate;
        color.A = 0.0f;
        Modulate = color;

        Tween tween = this.CreateTween();
        
        tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Circ);

        tween.TweenProperty(this, "modulate:a", 1.0f, 0.3f);
        tween.SetParallel();
        tween.TweenProperty(this, "scale:y", 1.0f, 0.3f).From(1.5f);

        // if((int)MovementType % 2 != 0)
        // {
        //     tween.TweenProperty(this, "rotation", GlobalRotation + Mathf.Pi * 8, 5.0f);
        //     tween.SetLoops();

        // }

    }

    public void SetCible(Node2D InCible)
    {
        Cible = InCible;
    }

    public void Collide(Node2D InEntered)
    {
        DcmProjectile parent = (DcmProjectile)GetParent();
        parent.EnsureValid().Collide(this, InEntered);
    }

    public void Die()
    {
        if (!_isDying)
        {
            _isDying = true;
            tw?.Stop();
            Poursuite.Velocity = 60.0f; 
            AnimatedSprite.Animation = "explode";
            AnimatedSprite.AnimationFinished += () => QueueFree();
        }


    }

    public void InfiniteTurn()
    {
        tw = AnimatedSprite.CreateTween();
        tw.TweenProperty(this, "rotation", GlobalRotation - Mathf.Pi * 8, 5.0f);
        tw.SetLoops();
    }

}

