using System;
using Godot;
using Utils;

public partial class Player : Node2D, ICollide, ITakeDamage
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
    private bool _player_won = false;
    private bool _is_game_started = false;
    public bool PlayerWon
    {
        get { return _player_won; }
        private set { _player_won = value; }
    }

    public bool IsDead
    {
        get { return _is_dead; }
        private set { _is_dead = value; }
    }
    public bool IsGameStarted
    {
        get { return _is_game_started; }
        set { _is_game_started = value; }
    }
    private string _currentDirection = "bas";

    public override void _Ready()
    {
        animatedPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void _Process(double delta)
    {
        if (_is_dead || _is_taking_damage || !_is_game_started)
            return;
        if (_player_won)
        {
            win();
            return;
        }
        else
        {
            HandleMouvementAnimation();
        }
    }

    private void HandleMouvementAnimation()
    {
        if (_is_dead || _player_won)
            return;

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

        Tween tween = CreateTween();
        tween.TweenProperty(this, "rotation", Rotation + Math.PI * 2.0f, 2.0);
    }

    public bool TakeDamage(int InDamage)
    {
        if (_is_dead)
        {
            return true;
        }
        if (healthBar != null)
        {
            healthBar.Value -= InDamage;
            if (healthBar.Value <= 0)
            {
                Die();
                return true;
            }
        }
        _is_taking_damage = true;
        animatedPlayer.Play($"dommage_{_currentDirection}");
        return false;
    }

    public void Die()
    {
        _is_dead = true;
        animatedPlayer.Play($"mort_{_currentDirection}");
    }

    public void win()
    {
        _player_won = true;
        animatedPlayer.Play("animation_victoire");
    }

    private void OnAnimationFinished()
    {
        if (((string)animatedPlayer.Animation).StartsWith("dommage"))
        {
            _is_taking_damage = false;
        }
        else if (((string)animatedPlayer.Animation).StartsWith("mort"))
        {
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
