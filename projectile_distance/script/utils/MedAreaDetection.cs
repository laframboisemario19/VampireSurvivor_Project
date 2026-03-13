using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Utils;

public partial class MedAreaDetection : Node2D
{
    public enum EAlgoSelectionDetection
    {
        eProjectileOnEnemy,
        eEnemyOnPlayer
    }

    public void Collide(EAlgoSelectionDetection InAlgoSelectionDetection, Node2D InEntering, Node2D InEntered)
    {
        switch (InAlgoSelectionDetection)
        {
            default:
            case EAlgoSelectionDetection.eProjectileOnEnemy:
                {
                    Projectile projectile = (Projectile)InEntering;
                    projectile.Die();
                    // InEntering.QueueFree();
                    // à ajouter la modif sur l'enemie
                }
                break;
            case EAlgoSelectionDetection.eEnemyOnPlayer:
                {
                    // à coder
                }
                break;
        }
    }

}
