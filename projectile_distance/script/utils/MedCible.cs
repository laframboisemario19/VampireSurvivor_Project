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
                    ret = SpawnerEnemy
                    .EnsureValid()
                    .GatherChildren()
                    .OfType<EnemyTest>()
                    .FirstOrDefault();
                }
                break;
        }
        return ret;
    }
}
