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
    private DcmEnemi SpawnerEnemy;

    [Export]
    private Player Player;

    public enum EAlgoSelectionCible
    {
        eClosestEnemy,
        ePlayer,
    }

    public Node2D ChoisirCible(EAlgoSelectionCible InAlgoSelectionCible, Vector2 InPosition)
    {
        Node2D ret = null;
        switch (InAlgoSelectionCible)
        {
            default:
            case EAlgoSelectionCible.ePlayer:
                {
                    ret = Player.EnsureValid();
                }
                break;
            case EAlgoSelectionCible.eClosestEnemy:
                {
                    IEnumerable<BaseEnemy> allEnemies = SpawnerEnemy
                        .EnsureValid()
                        .GatherChildren()
                        .OfType<BaseEnemy>();
                    ret = allEnemies.First();
                    float currentLength = (InPosition - ret.GlobalPosition).Length();
                    foreach (BaseEnemy enemy in allEnemies)
                    {
                        float length = (InPosition - enemy.GlobalPosition).Length();

                        if (length < currentLength)
                        {
                            ret = enemy;
                            currentLength = length;
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
