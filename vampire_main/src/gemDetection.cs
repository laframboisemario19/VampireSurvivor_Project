using System;
using Godot;

public partial class GemDetection : Area2D
{
    private void OnAreaEntered(Area2D InArea)
    {
        Node player = InArea.GetParent();

        if (player is Player p)
        {
            p.AddXp(20);
        }
    }
}
