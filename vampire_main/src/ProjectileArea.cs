using System;
using Godot;
using static MedAttack;

public partial class ProjectileArea : Area2D, IAreaDetection
{
    [Export]
    Projectile InProjectile;

    public void _on_area_entered(Area2D InArea)
    {
        InProjectile.Collide((Node2D)InArea.GetParent());
    }
}
