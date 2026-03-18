using System;
using Godot;

public partial class playerCollision : Area2D
{
    [Export]
    Node2D playerNode;

    private bool _takingDamage = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (playerNode is animationPlayer) { }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void OnAreaEntered(Area2D InArea) { }
}
