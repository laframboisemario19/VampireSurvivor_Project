using System;
using Godot;

public partial class BaseObject : Node2D, IAnimate
{
    public int XpValue { get; protected set; }

    public virtual void Animate(Player InPlayer) { }

    public void Die()
    {
        QueueFree();
    }
}
