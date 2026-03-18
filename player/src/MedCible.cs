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
                    // à coder
                    ret = Player.EnsureValid();
                }
                break;
            case EAlgoSelectionCible.eClosestEnemy:
                {
                    IEnumerable<Zombie> allEnemies = SpawnerEnemy
                        .EnsureValid()
                        .GatherChildren()
                        .OfType<Zombie>();
                    ret = allEnemies.First();
                    float currentength = (InPosition - ret.GlobalPosition).Length();
                    foreach (Zombie enemy in allEnemies)
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
