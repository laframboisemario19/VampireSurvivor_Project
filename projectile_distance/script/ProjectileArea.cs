using Godot;
using System;
using static MedAreaDetection;

public partial class ProjectileArea : Area2D, IAreaDetection
{
    [Export]
    Projectile InProjectile;
    public void _on_area_entered(Area2D InArea)
    {
        if (InArea.GetParent() is EnemyTest enemy)
        {
            InProjectile.Colide(enemy);
        }
        
    }

}
