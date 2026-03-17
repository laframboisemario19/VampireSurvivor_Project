using Godot;
using System;

public partial class Sword : Sprite2D
{
	[Export]
	private Sprite2D node;
	[Export]
	private Sprite2D AnimLeft;
	[Export]
	private Sprite2D AnimRight;

	private Tween tween;
	private Vector2 newScale = new Vector2();

	private float amplitude = Mathf.Pi / 2;
	private bool _usePatternA = true;

	public override void _Ready()
	{
		Swing();
	}

	public void Swing()
	{
		if (!IsInstanceValid(node))
			return;

		tween?.Kill();

		tween = CreateTween();

		float baseRotation = node.Rotation;
		Scale = new Vector2(1.6f, 1.6f);
		newScale = new Vector2(2.0f, 2.0f);

		AnimLeft.Modulate = new Color(1, 1, 1, 0);
		AnimRight.Modulate = new Color(1, 1, 1, 0);


		float a1, a2, a3;

		if (_usePatternA)
		{
			a1 = +amplitude;
			a2 = -amplitude;
			a3 = +amplitude;

			tween.TweenProperty(node, "scale", newScale, 0.3f);
			tween.TweenProperty(AnimRight, "modulate:a", 1.0f, 0.02f);
			CreateHitTween(baseRotation + a1);
			tween.TweenProperty(AnimRight, "modulate:a", 0.0f, 0.02f);
			tween.TweenProperty(AnimLeft, "modulate:a", 1.0f, 0.02f);
			CreateHitTween(baseRotation + a2);
			tween.TweenProperty(AnimLeft, "modulate:a", 0.0f, 0.02f);
			tween.TweenProperty(AnimRight, "modulate:a", 1.0f, 0.02f);
			CreateHitTween(baseRotation + a3);
			tween.TweenProperty(AnimRight, "modulate:a", 0.0f, 0.02f);
			tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);
		}
		else
		{
			a1 = -amplitude;
			a2 = +amplitude;
			a3 = -amplitude;

			tween.TweenProperty(node, "scale", newScale, 0.3f);
			tween.TweenProperty(AnimLeft, "modulate:a", 1.0f, 0.02f);
			CreateHitTween(baseRotation + a1);
			tween.TweenProperty(AnimLeft, "modulate:a", 0.0f, 0.02f);
			tween.TweenProperty(AnimRight, "modulate:a", 1.0f, 0.02f);
			CreateHitTween(baseRotation + a2);
			tween.TweenProperty(AnimRight, "modulate:a", 0.0f, 0.02f);
			tween.TweenProperty(AnimLeft, "modulate:a", 1.0f, 0.02f);
			CreateHitTween(baseRotation + a3);
			tween.TweenProperty(AnimLeft, "modulate:a", 0.0f, 0.02f);
			tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);
		}

		_usePatternA = !_usePatternA;
	}


	private void CreateHitTween(float rotation)
	{
		tween.TweenProperty(node, "rotation", rotation, 0.12f);
		tween.TweenProperty(node, "rotation", 0f, 0.12f);
	}
}