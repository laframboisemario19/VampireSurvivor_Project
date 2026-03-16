using Godot;
using System;

public partial class Boxe : Sprite2D
{
	[Export] private Sprite2D node;

	[Export] private Sprite2D hitEffect;

	[Export] private DcmAttack attackManager;
	[Export] private Timer timer;

	private Tween tween;

	private int _directionIndex = 0;

	private Vector2[] directions =
	{
		Vector2.Right,
		Vector2.Down,
		Vector2.Left,
		Vector2.Up
	};

	int cpt = 0;


	public override void _Ready()
	{
		base._Ready();
		timer.Timeout += Attack;
	}

	private void Attack()
	{
		if (!attackManager.TryAttacking())
		{
			return;
		}
		else
		{
			Hit();
		}
	}

	public void Hit()
	{
		if (!IsInstanceValid(node))
			return;

		Visible = true;

		tween?.Kill();

		node.Scale = Vector2.Zero;
		node.Modulate = new Color(1, 1, 1, 0);
		hitEffect.Modulate = new Color(1, 1, 1, 0);

		tween = CreateTween();

		Vector2 dir = directions[_directionIndex];

		node.Rotation = dir.Angle() - Mathf.Pi / 2;

		for (int i = 0; i < 4; i++)
		{
			CreatePunch(dir);
		}

		_directionIndex++;

		tween.Finished += FinAttaque;

	}

	private void CreatePunch(Vector2 dir)
	{
		Vector2 start = node.Position;

		Vector2 hit = start + dir * 30;

		Vector2 punchScale = new Vector2(0.1f, 0.1f);

		tween.TweenProperty(node, "scale", punchScale, 0.05f);
		tween.TweenProperty(node, "modulate:a", 0.5f, 0.05f);

		tween.TweenProperty(node, "position", hit, 0.07f);
		tween.TweenProperty(hitEffect, "modulate:a", 0.5f, 0.02f);

		tween.TweenProperty(node, "position", start, 0.07f);
		tween.TweenProperty(hitEffect, "modulate:a", 0.0f, 0.02f);

		tween.TweenProperty(node, "scale", Vector2.Zero, 0.05f);
		tween.TweenProperty(node, "modulate:a", 0.0f, 0.05f);

		tween.TweenInterval(0.05f);
	}

	private void FinAttaque()
	{
		Visible = false;
		attackManager.EndAttacking();
	}
}
