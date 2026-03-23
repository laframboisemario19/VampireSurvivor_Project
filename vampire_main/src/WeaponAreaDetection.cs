using System;
using Godot;
using Utils;
using static MedAttack;

public partial class WeaponAreaDetection : Area2D, IAreaDetection
{
    [Export]
    private Node2D Weapon;

    [Export]
    private EAlgoSelectionDetection eAlgoSelectionDetection;

    public void _on_area_entered(Area2D InArea)
    {
        Weapon.EnsureValid();
        ((ICollide)Weapon).Collide(eAlgoSelectionDetection, (Node2D)InArea.GetParent(), Weapon);
    }
}
