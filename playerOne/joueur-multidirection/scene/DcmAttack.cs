using Godot;
using System;

public partial class DcmAttack : Node2D
{
	[Export]
	private Sword sword;
	[Export]
	private Axe axe;
	[Export]
	private Boxe boxe;

	public void AttackEpee()
	{
		sword.Swing();
	}

	public void AttackHache()
	{
		axe.Swing();
	}

	public void AttackBoxe()
	{
		boxe.Hit();
	}
}
