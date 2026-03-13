using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Utils;

public partial class MedCible : Node2D
{
    [ExportGroup("External")]
    [Export]
    private DcmProjectile SpawnerProjectile; 
    [Export]
    private DcmEnemyTest SpawnerEnemy;
    [Export]
    private PlayerTest Player;

    public enum EAlgoSelectionCible
    {
        eClosestEnemy,
        ePlayer
    }

    public Node2D ChoisirCible(EAlgoSelectionCible InAlgoSelectionCible, Vector2 InPosition)
    {
        Node2D ret = null;
        switch (InAlgoSelectionCible)
        {
            default:
            case EAlgoSelectionCible.ePlayer:
                {
                    // à coder
                    ret = null;
                }
                break;
            case EAlgoSelectionCible.eClosestEnemy:
                {
                    IEnumerable<EnemyTest> allEnemies = SpawnerEnemy.EnsureValid().GatherChildren().OfType<EnemyTest>();
                    ret = allEnemies.First();
                    Vector2 currentDistance = InPosition - ret.GlobalPosition;
                    foreach (EnemyTest enemy in allEnemies)
                    {
                        Vector2 distance = InPosition - enemy.GlobalPosition;
                        if (distance < currentDistance)
                        {
                            ret = enemy;
                            currentDistance = distance;
                        }
                    }
                }
                break;
        }
        return ret;
    }

    public Vector2 GetPlayerPosition()
    {
        return Player.EnsureValid().GlobalPosition;
    }
}
