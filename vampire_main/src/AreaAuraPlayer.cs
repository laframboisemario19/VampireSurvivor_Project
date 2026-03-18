using System;
using Godot;
using static MedAttack;

public partial class AreaAuraPlayer : Area2D
{
    [Export]
    Player Player;

    public void _on_area_entered(Area2D InArea)
    {
        EAlgoSelectionDetection algo = EAlgoSelectionDetection.eTreasureOnAuraPlayer;
        ((ICollide)Player).Collide(algo, (Node2D)InArea.GetParent(), Player);
    }
}
