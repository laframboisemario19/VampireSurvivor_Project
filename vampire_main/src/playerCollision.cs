using System;
using Godot;

public partial class playerCollision : Area2D
{
    [Export]
    Node2D playerNode;

    private bool _takingDamage = false;

    public override void _Ready()
    {
        if (playerNode is animationPlayer) { }
    }

    public override void _Process(double delta) { }

    public void OnAreaEntered(Area2D InArea) { }
}
