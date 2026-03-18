using Godot;
using System;

public partial class Ennemi2 : Node2D
{
	public int healthPoints = 100; 

	public void TakeDamage(int damage)
	{
		healthPoints -= damage;
		GD.Print("Ennemi2 HP: " + healthPoints);
	}
}
