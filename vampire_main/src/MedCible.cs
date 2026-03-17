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
    private Player Player;

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
                    float currentength = (InPosition - ret.GlobalPosition).Length();
                    foreach (EnemyTest enemy in allEnemies)
                    {
                        float length = (InPosition - enemy.GlobalPosition).Length();
                    
                        if (length < currentength)
                        {
                            ret = enemy;
                            currentength = length;
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
