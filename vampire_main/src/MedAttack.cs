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
    int i = 6;

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
                    Zombie zombie = (Zombie)InEntered;
                    zombie.Die();
                    // à ajouter la modif sur l'enemie
                }
                break;
            case EAlgoSelectionDetection.eEnemyOnPlayer:
                {
                    // à coder
                    GD.Print("Ouch!");
                }
                break;
            case EAlgoSelectionDetection.eMeleeOnEnemy:
                {
                    Zombie zombie = (Zombie)InEntering;
                    zombie.Die();
                }
                break;
        }
    }
}
