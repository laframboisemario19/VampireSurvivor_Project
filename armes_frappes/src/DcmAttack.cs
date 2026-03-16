using Godot;
using System;

public partial class DcmAttack : Node2D
{	
	[ExportGroup("External")]
	[Export] private Sword sword;
	[Export] private Axe axe;
	[Export] private Boxe boxe;

	private bool IsAttacking = false;

	public bool TryAttacking()
	{
		if (IsAttacking)
		{
			return false;
		}
		else
		{
			IsAttacking = true;
			return true;
		}
	}

	public void EndAttacking()
	{
		IsAttacking = false;
	}


    public override void _Ready()
    {
        base._Ready();
    }


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
