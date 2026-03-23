using System;
using Godot;

public partial class MouvementPlayer : Node2D
{
    [Export]
    private Node2D NodeToControl;

    [Export]
    private float VelocityPixelPerSecond = 100.0f;

    private bool _isActive = true;

    [Export]
    public Player Player;

    [Export]
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;

            SetProcess(value);
            SetPhysicsProcess(value);
        }
    }

    Vector2 _inputVector = new(0.0f, 0.0f);

    public override void _Process(double InDelta)
    {
        base._Process(InDelta);
        if (!Player.IsGameStarted || Player.PlayerWon)
            return;
        _inputVector = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (_inputVector.Length() < 1.0f)
        {
            return;
        }
        _inputVector = _inputVector.Normalized();
    }

    public override void _PhysicsProcess(double InDelta)
    {
        base._PhysicsProcess(InDelta);
        if (NodeToControl is null || NodeToControl is Player p && p.IsDead)
        {
            return;
        }
        NodeToControl.Position += VelocityPixelPerSecond * (float)InDelta * _inputVector;
    }
}
