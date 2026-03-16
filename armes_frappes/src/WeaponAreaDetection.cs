using Godot;
using System;

public partial class WeaponAreaDetection : Area2D
{

	public void OnAreaEntered(Area2D InArea)
	{
		Node enemy = InArea.GetParent();

		if(enemy is Ennemi1 e1)
		{
			GD.Print("Ennemi1 Touchés !");
			e1.TakeDamage(10);
		}
		else if (enemy is Ennemi2 e2)
		{
			e2.TakeDamage(10);
		}
		
	}
}
