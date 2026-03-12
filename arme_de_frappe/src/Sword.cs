using Godot;
using System;

public partial class Sword : Sprite2D
{
	[Export]
	private Sprite2D node;

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

		float a1, a2, a3;

		if (_usePatternA)
		{
			a1 = +amplitude;
			a2 = -amplitude;
			a3 = +amplitude;
		}
		else
		{
			a1 = -amplitude;
			a2 = +amplitude;
			a3 = -amplitude;
		}

		tween.TweenProperty(node, "scale", newScale, 0.3f);
		CreateHitTween(baseRotation + a1);
		CreateHitTween(baseRotation + a2);
		CreateHitTween(baseRotation + a3);
		tween.TweenProperty(node, "scale", Vector2.Zero, 0.3f);

		_usePatternA = !_usePatternA;
	}


	private void CreateHitTween(float rotation)
	{
		tween.TweenProperty(node, "rotation", rotation, 0.12f);
		tween.TweenProperty(node, "rotation", 0f, 0.12f);
	}
}