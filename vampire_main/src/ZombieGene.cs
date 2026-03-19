using System;
using System.Reflection.PortableExecutable;
using Godot;

public partial class ZombieGene : BaseEnemy
{
    private Vector2 minDistance = new(10, 10);

    public void Stop()
    {
        if (Cible.GlobalPosition - this.GlobalPosition <= minDistance)
        {
            isDying = true;
            Area.Die();
            Poursuite.Velocity = 60.0f;
            AnimatedSprite.Play("explode");
            AnimatedSprite.AnimationFinished += () => QueueFree();
        }
    }
}
