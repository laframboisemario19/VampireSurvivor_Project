using System;
using Godot;
using Utils;
using static MedAttack;

public partial class Sword : BaseWeapon, ICollide
{
    [Export]
    private Player Player;

    [Export]
    private Sprite2D node;

    [Export]
    private Area2D SwordArea;

    [Export]
    private Sprite2D AnimLeft;

    [Export]
    private Sprite2D AnimRight;

    [Export]
    private DcmAttack attackManager;

    [Export]
    private Timer timer;

    private Tween tween;
    private Vector2 newScale = new Vector2();

    private bool _usePatternA = true;

    public override void ActivateAttack()
    {
        timer.Timeout += Swing;
    }

    public void Swing()
    {
        if (!IsInstanceValid(node))
            return;

        Visible = true;
        SwordArea.SetCollisionMaskValue(2, true);

        tween?.Kill();

        tween = CreateTween();

        Scale = new Vector2(0.0f, 0.0f);
        newScale = new Vector2(0.5f, 0.5f);

        AnimLeft.Modulate = new Color(1, 1, 1, 0);
        AnimRight.Modulate = new Color(1, 1, 1, 0);

        float left = Mathf.Pi;
        float right = 0.0f;
        

        if (_usePatternA)
        {
            tween.TweenProperty(node, "scale", newScale, 0.3f);

            tween.TweenProperty(AnimRight, "modulate:a", 1.0f, 0.02f);
            CreateHitTween(left);
            tween.TweenProperty(AnimRight, "modulate:a", 0.0f, 0.02f);

            tween.TweenProperty(AnimLeft, "modulate:a", 1.0f, 0.02f);
            CreateHitTween(right);
            tween.TweenProperty(AnimLeft, "modulate:a", 0.0f, 0.02f);

            tween.TweenProperty(AnimRight, "modulate:a", 1.0f, 0.02f);
            CreateHitTween(left);
            tween.TweenProperty(AnimRight, "modulate:a", 0.0f, 0.02f);

            tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);
        }
        else
        {
            tween.TweenProperty(node, "scale", newScale, 0.3f);

            tween.TweenProperty(AnimLeft, "modulate:a", 1.0f, 0.02f);
            CreateHitTween(right);
            tween.TweenProperty(AnimLeft, "modulate:a", 0.0f, 0.02f);

            tween.TweenProperty(AnimRight, "modulate:a", 1.0f, 0.02f);
            CreateHitTween(left);
            tween.TweenProperty(AnimRight, "modulate:a", 0.0f, 0.02f);

            tween.TweenProperty(AnimLeft, "modulate:a", 1.0f, 0.02f);
            CreateHitTween(right);
            tween.TweenProperty(AnimLeft, "modulate:a", 0.0f, 0.02f);

            tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);
        }

        _usePatternA = !_usePatternA;

        tween.Finished += FinAttaque;
    }

    private void CreateHitTween(float rotation)
    {
        tween.TweenProperty(node, "rotation", rotation, 0.12f);
    }

    private void FinAttaque()
    {
        Visible = false;
        SwordArea.SetCollisionMaskValue(2, false);
        attackManager.EndAttacking();
    }

    public void Collide(
        EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        ((ICollide)(Player.EnsureValid())).Collide(InAlgoSelectionDetection, InEntering, InEntered);
    }
}
