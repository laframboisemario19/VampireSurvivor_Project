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
            case 2:
                {
                    algo = EAlgoSelectionDetection.eEnemyOnPlayer;
                }
                break;
            case 4:
                {
                    algo = EAlgoSelectionDetection.eMapOnPlayer;
                }
                break;
            case 8:
                {
                    algo = EAlgoSelectionDetection.eTreasureOnPlayer;
                }
                break;
            default:
                return;
        }
        ((ICollide)Player).Collide(algo, (Node2D)InArea.GetParent(), Player);
    }
}
