using System;
using Godot;
using static MedAttack;

public partial class Trap : BaseObject, ICollide, ITakeDamage
{
    [ExportGroup("External")]
    [Export]
    private DcmObject dcmObject;
    private int health = 5;

    public override void _Ready()
    {
        base._Ready();
        base.XpValue = 0;
        dcmObject = (DcmObject)GetParent();
    }

    public void Collide(
        MedAttack.EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        ((ICollide)dcmObject).Collide(InAlgoSelectionDetection, InEntering, InEntered);
    }

    public bool TakeDamage(int InDamage)
    {
        health -= InDamage;
        if (health <= 0)
        {
            QueueFree();
            return true;
        }
        return true;
    }
}
