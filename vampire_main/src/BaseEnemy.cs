using System;
using Godot;
using Utils;

public partial class BaseEnemy : Node2D, ICiblable, ITakeDamage
{
    [ExportGroup("External")]
    [Export]
    public Node2D Cible
    {
        get => this.Poursuite.EnsureValid().GetCible();
        set { this.Poursuite.EnsureValid().SetCible(value); }
    }

    [ExportGroup("Internal")]
    [Export]
    protected EnemyArea Area;

    [Export]
    protected Poursuite Poursuite;

    [Export]
    protected AnimatedSprite2D AnimatedSprite;
    public bool isDying = false;

    [Export(PropertyHint.Range, "0,50,1")]
    private int hp = 5;

    public virtual bool TakeDamage(int InDamage)
    {
        if (isDying)
            return false;

        Vector2 posPlayer = Cible.GlobalPosition;
        Vector2 posSelf = this.GlobalPosition;
        Vector2 offset = posSelf + ((posSelf - posPlayer).Normalized() * 10.0f);
        Color baseMod = Modulate;

        GD.Print($"Enemi old position : {posSelf}");
        GD.Print($"Enemi new position : {offset}");

        Tween tween = CreateTween();
        tween
            .TweenProperty(this, "position", offset, 0.5f)
            .SetTrans(Tween.TransitionType.Back)
            .SetEase(Tween.EaseType.InOut);
        tween
            .Parallel()
            .TweenProperty(this, "modulate:a", 0.2f, 1.0f)
            .SetTrans(Tween.TransitionType.Bounce)
            .SetEase(Tween.EaseType.OutIn);
        tween.Chain().TweenProperty(this, "modulate:a", 1.0, 0.1f);

        hp -= InDamage;
        GD.Print($"Enemy {GetHashCode()} hit! HP: {hp}");

        if (hp <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    public virtual void Die()
    {
        if (!isDying)
        {
            isDying = true;
            Area.Die();
            Poursuite.Velocity = 60.0f;
            AnimatedSprite.Play("explode");
            AnimatedSprite.AnimationFinished += () => QueueFree();
        }
    }

    public void SetCible(Node2D InCible)
    {
        Cible = InCible;
    }
}
