using System;
using Godot;
using static MedAttack;

public partial class PlayerArea : Area2D
{
    [Export]
    Player Player;

    public void _on_area_entered(Area2D InArea)
    {
        EAlgoSelectionDetection algo;
        switch (InArea.CollisionLayer)
        {
            default:
            case 2: // layer 2 = Enemy
                {
                    algo = EAlgoSelectionDetection.eEnemyOnPlayer;
                }
                break;
        }
        ((ICollide)Player).Collide(algo, (Node2D)InArea.GetParent(), Player);
    }
}
