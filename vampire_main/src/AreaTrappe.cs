using System;
using Godot;
using static MedAttack;

public partial class AreaTrappe : Area2D
{
    [ExportGroup("Internal")]
    [Export]
    private AnimatedSprite2D InSprite;

    [Export]
    private Node2D Trappe;

    public void _on_area_entered(Area2D InArea)
    {
        InSprite.Play("activate");
        ((ICollide)Trappe).Collide(
            EAlgoSelectionDetection.eTrapOnCharacter,
            Trappe,
            (Node2D)InArea.GetParent()
        );
    }
}
