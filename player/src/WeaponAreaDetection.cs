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

    public void OnAreaEntered(Area2D InArea)
    {
        // Node enemy = InArea.GetParent();

        // if (enemy is Ennemi1 e1)
        // {
        //     GD.Print("Ennemi1 Touchés !");
        //     e1.TakeDamage(10);
        // }
        // else if (enemy is Ennemi2 e2)
        // {
        //     e2.TakeDamage(10);
        // }
    }

    public void _on_area_entered(Area2D InArea)
    {
        Weapon.EnsureValid();
        ((ICollide)Weapon).Collide(eAlgoSelectionDetection, (Node2D)InArea.GetParent(), Weapon);
    }
}
