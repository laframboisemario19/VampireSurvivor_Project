using System;
using System.Collections;
using Godot;

public partial class DcmAttack : Node2D
{
    [ExportGroup("External")]
    [Export]
    private BaseWeapon sword,
        axe,
        boxe;

    public enum eWeaponType
    {
        eSword = 6,
        eAxe = 7,
        eBoxe = 8,
    }

    ArrayList attackList = [];

    public override void _Ready()
    {
        base._Ready();
        attackList.AddRange(new[] { sword, axe, boxe });
    }

    public void ActivateAttack(eWeaponType WeaponType)
    {
        ((BaseWeapon)attackList[(int)WeaponType - 6]).ActivateAttack();
    }

    private bool IsAttacking = false;

    public void EndAttacking()
    {
        IsAttacking = false;
    }

    public void GameOver()
    {
        ((Sword)sword).GameOver();
        ((Axe)axe).GameOver();
        ((Boxe)boxe).GameOver();
    }
}
