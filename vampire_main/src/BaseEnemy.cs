using System;
using Godot;

public partial class BaseEnemy : Node2D
{
    [ExportGroup("Internal")]
    [Export]
    protected EnemyArea Area;

    [Export]
    protected Poursuite Poursuite;

    [Export]
    protected AnimatedSprite2D AnimatedSprite;
    public bool isDying = false;

    public void Die()
    {
        if (!isDying)
        {
            isDying = true;
            Area.Die();
            Poursuite.Velocity = 60.0f;
            AnimatedSprite.Play("explode");
            AnimatedSprite.AnimationFinished += () => QueueFree();
        }
    }
}
