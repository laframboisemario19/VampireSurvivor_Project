using Godot;
using System;
using static MedAreaDetection;
using Utils;

public partial class Projectile : Node2D, ICiblable
{
    [ExportGroup("External")]
    [Export]
    public Node2D Cible
    {
        get => Poursuite.GetCible();
        set { Poursuite.SetCible(value); }
    }

    [ExportGroup("Internal")]
    [Export]
    Poursuite Poursuite;

    public void SetCible(Node2D InCible)
    {
        Cible = (Node2D)InCible;
    }

    public void Colide(Node2D InEntered)
    {
        DcmProjectile parent = (DcmProjectile)GetParent();
        parent.EnsureValid().Colide(this, InEntered);
    }
}
