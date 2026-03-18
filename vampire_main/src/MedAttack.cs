using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Utils;
using static DcmProjectile;

public partial class MedAttack : Node2D, ICollide
{
    [Export]
    private Timer timer;

    [Export]
    private DcmProjectile DcmProjectile;
    int i = 0;

    public enum EAlgoSelectionDetection
    {
        eProjectileOnEnemy,
        eEnemyOnPlayer,
        eMeleeOnEnemy,
    }

    public override void _Ready()
    {
        base._Ready();

        // À remplacer par une fonction permettant le choix d'arme
        timer.EnsureValid().Timeout += () =>
        {
            if (i < 6)
            {
                DcmProjectile.ActivateProjectile((EProjectileType)i);
                i++;
            }
        };
    }

    public void Collide(
        EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        switch (InAlgoSelectionDetection)
        {
            default:
            case EAlgoSelectionDetection.eProjectileOnEnemy:
                {
                    Projectile projectile = (Projectile)InEntering;
                    projectile.Die();
                    BaseEnemy enemy = (BaseEnemy)InEntered;
                    enemy.Die();
                    // à ajouter la modif sur l'enemie
                }
                break;
            case EAlgoSelectionDetection.eEnemyOnPlayer:
                {
                    // à coder
                    int damage = 0;
                    if (InEntered is Player player)
                    {
                        if (InEntering != null)
                        {
                            damage = 1;
                        }
                        player.TakeDamage(damage);
                    }
                }
                break;
            case EAlgoSelectionDetection.eMeleeOnEnemy:
                {
                    BaseEnemy enemy = (BaseEnemy)InEntering;
                    enemy.Die();
                }
                break;
        }
    }
}
