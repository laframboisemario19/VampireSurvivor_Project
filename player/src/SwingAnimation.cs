using Godot;
using System;

public partial class SwingAnimation : Sprite2D
{
	[Export] private Sprite2D swing;

	[Export] private Timer timer;

	private Tween tween;
	private Vector2 newScale = new Vector2();


	public override void _Ready()
	{
		base._Ready();
		timer.Timeout += Swing;

	}

	public void Swing()
	{
		tween?.Kill();

		Scale = Vector2.Zero;
		newScale = new Vector2(1.0f, 1.0f);
		swing.Modulate = new Color(1, 1, 1, 1);

		tween = CreateTween();

		tween.TweenInterval(0.5f);
		tween.TweenProperty(swing, "scale", newScale, 0.3f);
		tween.TweenInterval(0.4f);

		tween.TweenProperty(swing, "modulate:a", 0.0f, 0.02f);

		// tween.Finished += FinAttaque;

	}

	// private void FinAttaque()
	// {
	// 	Swing();
	// }
}
