using System;
using Godot;

public partial class Trap : BaseObject
{
    public override void _Ready()
    {
        base._Ready();
        base.XpValue = 0;
    }
}
