using System;
using Godot;
using Utils;

public partial class Player : Node2D, ICollide
{
    [ExportGroup("External")]
    [Export]
    MedAttack InMedAttack;

    [Export]
    private TextureProgressBar XpBar;

    [ExportGroup("Internal")]
    [Export]
    AnimatedSprite2D animatedPlayer;

    [Export]
    TextureProgressBar healthBar;

    [Export]
    public DcmAttack DcmAttack { get; private set; }

    private bool _is_taking_damage = false;
    private bool _is_dead = false;
    public bool IsDead
    {
        get { return _is_dead; }
        private set { _is_dead = value; }
    }
    private string _currentDirection = "bas";

    public override void _Ready()
    {
        animatedPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void _Process(double delta)
    {
        if (_is_dead || _is_taking_damage)
            return;

        HandleMouvementAnimation();
    }

    private void HandleMouvementAnimation()
    {
        string newAnim = "";

        if (Input.IsActionPressed("ui_right"))
        {
            newAnim = "marche_droite";
            _currentDirection = "droite";
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            newAnim = "marche_gauche";
            _currentDirection = "gauche";
        }
        else if (Input.IsActionPressed("ui_up"))
        {
            newAnim = "marche_haut";
            _currentDirection = "haut";
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            newAnim = "marche_bas";
            _currentDirection = "bas";
        }

        if (newAnim != "")
        {
            animatedPlayer.Play(newAnim);
        }
        else
        {
            animatedPlayer.Play($"marche_{_currentDirection}");
            animatedPlayer.Stop();
        }
    }

    // public override void _Process(double delta)
    // {
    //     if (Input.IsActionPressed("ui_right"))
    //     {
    //         animatedPlayer.Play("marche_droite");
    //     }
    //     else if (Input.IsActionPressed("ui_left"))
    //     {
    //         animatedPlayer.Play("marche_gauche");
    //     }
    //     else if (Input.IsActionPressed("ui_up"))
    //     {
    //         animatedPlayer.Play("marche_haut");
    //     }
    //     else if (Input.IsActionPressed("ui_down"))
    //     {
    //         animatedPlayer.Play("marche_bas");
    //     }
    //     else
    //     {
    //         animatedPlayer.Play("marche_bas");
    //         animatedPlayer.Stop();
    //     }
    // }

    public bool AddXp(int value)
    {
        if (XpBar != null)
        {
            XpBar.Value += value;
            if (XpBar.Value == XpBar.MaxValue)
            {
                _LevelUp();
                return true;
            }
        }
        return false;
    }

    private void _LevelUp()
    {
        XpBar.Value = 0;
        XpBar.MaxValue = (int)(XpBar.MaxValue * 1.2);
    }

    public void TakeDamage(int damage)
    {
        if (_is_dead)
        {
            return;
        }
        if (healthBar != null)
        {
            healthBar.Value -= damage;
            if (healthBar.Value <= 0)
            {
                Die();
                return;
            }
        }
        _is_taking_damage = true;
        animatedPlayer.Play($"dommage_{_currentDirection}");
    }

    public void Die()
    {
        _is_dead = true;
        animatedPlayer.Play($"mort_{_currentDirection}");
    }

    private void OnAnimationFinished()
    {
        GD.Print("Animation terminée : " + animatedPlayer.Animation);
        if (((string)animatedPlayer.Animation).StartsWith("dommage"))
        {
            _is_taking_damage = false;
        }
        else if (((string)animatedPlayer.Animation).StartsWith("mort"))
        {
            // QueueFree();
            GetTree().Quit();
        }
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
