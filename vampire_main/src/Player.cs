using System;
using Godot;
using Utils;

public partial class Player : Node2D, ICollide
{
    [ExportGroup("External")]
    [Export]
    MedAttack InMedAttack;

    [Export]
    private TextureProgressBar t;

    [Export]
    AnimatedSprite2D animatedPlayer;

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("ui_right"))
        {
            animatedPlayer.Play("marche_droite");
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            animatedPlayer.Play("marche_gauche");
        }
        else if (Input.IsActionPressed("ui_up"))
        {
            animatedPlayer.Play("marche_haut");
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            animatedPlayer.Play("marche_bas");
        }
        else
        {
            animatedPlayer.Play("marche_bas");
            animatedPlayer.Stop();
        }
    }

    public void AddXp(int value)
    {
        t.Value += value;
    }

    public void Collide(
        MedAttack.EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        ((ICollide)InMedAttack.EnsureValid()).Collide(
            InAlgoSelectionDetection,
            InEntering,
            InEntered
        );
    }
}
