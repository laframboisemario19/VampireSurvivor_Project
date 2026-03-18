using System;
using Godot;
using static MedAttack;

public interface ICollide
{
    public void Collide(
        EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    );
}
