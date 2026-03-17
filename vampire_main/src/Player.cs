using Godot;
using System;

public partial class Player : Node2D
{

	[Export] private TextureProgressBar t;

	public void AddXp(int value)
	{
		t.Value += value;
	}
}
