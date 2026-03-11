using Godot;
using System;

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
}
