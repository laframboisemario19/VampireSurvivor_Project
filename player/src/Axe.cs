using Godot;
using System;

public partial class Axe : Sprite2D
{
	[Export]
	private Sprite2D node;

	private Tween tween;
	private Vector2 newScale = new Vector2();

	public override void _Ready()
	{
		base._Ready();
		Swing();

	}

	public void Swing()
	{
		tween?.Kill();

		Scale = new Vector2(1.6f, 1.6f);
		newScale = new Vector2(2.0f, 2.0f);

		tween = CreateTween();


		tween.TweenProperty(node, "scale", newScale, 0.3f);
		tween.TweenInterval(0.2f);

		tween.TweenProperty(node, "rotation", Mathf.Tau, 0.6f);
		tween.TweenInterval(0.3f);

		tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);

		tween.Finished += FinAttaque;
	}

	private void FinAttaque()
	{
		Rotation = 0;
	}
}