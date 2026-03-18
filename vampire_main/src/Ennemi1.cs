using System;
using Godot;

public partial class Ennemi1 : Node2D
{
    public int healthPoints = 50;

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        GD.Print("Ennemi1 HP: " + healthPoints);
    }
}
