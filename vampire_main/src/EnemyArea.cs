using System;
using Godot;

public partial class EnemyArea : Area2D
{
    public void Die()
    {
        SetCollisionLayerValue(2, false);
    }
}
