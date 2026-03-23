using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using Utils;

public class ChildManipulator
{
    public static IEnumerable<Node2D> GatherChildren(
        PackedScene InPackedSceneToFilter,
        Node2D InDCMFilter
    )
    {
        InPackedSceneToFilter.EnsureValid();
        InDCMFilter.EnsureValid();
        SceneState state = InPackedSceneToFilter.GetState();
        StringName nodeType = state.GetNodeType(0);
        Array<Node> ret = InDCMFilter.FindChildren("*", nodeType, false, false);
        return ret.OfType<Node2D>();
    }
}
