using System;
using System.Runtime.CompilerServices;
using Godot;
using Utils;
using static MedAttack;

public partial class Axe : BaseWeapon, ICollide
{
    [Export]
    private Player Player;

    [Export]
    private Sprite2D node;

    [Export]
    private Area2D AxeArea;

    [Export]
    private DcmAttack attackManager;

    [Export]
    private Timer timer;



    private Tween tween;
    private Vector2 newScale = new Vector2();

    public override void ActivateAttack()
    {
        timer.Timeout += Swing;
    }

    public void Swing()
    {
        if (!IsInstanceValid(node))
            return;

        Visible = true;
        AxeArea.SetCollisionMaskValue(2, true);

        tween?.Kill();

        Scale = new Vector2(0.0f, 0.0f);
        newScale = new Vector2(1.0f, 1.0f);

        tween = CreateTween();

        tween.TweenProperty(node, "scale", newScale, 0.3f);
        tween.TweenInterval(0.2f);

        tween.TweenProperty(node, "rotation", Mathf.Tau, 0.6f);
        tween.TweenInterval(0.3f);

        tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);

        tween.Finished += FinAttaque;
    }

    private void FinAttaque()
    {
        Rotation = 0;
        Visible = false;
        AxeArea.SetCollisionMaskValue(2, false);
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
