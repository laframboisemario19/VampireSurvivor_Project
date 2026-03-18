using Godot;
using System;
using System.Collections.Generic;

public partial class DcmEnemyTest : Node2D
{
	[ExportGroup("External")]
    [Export]
    private PackedScene SpawneeScene;

    public IEnumerable<Node2D> GatherChildren()
    {
        return ChildManipulator.GatherChildren(SpawneeScene, this);
    }
}
