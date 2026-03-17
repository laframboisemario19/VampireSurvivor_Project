using Godot;
using System;
using Utils;

public interface ISpawn
{
    public Node2D Spawn(PackedScene SpawneeScene)
    {
        SpawneeScene.EnsureValid();
        Node2D newInstance = SpawneeScene.Instantiate<Node2D>();
        newInstance.EnsureValid();
        return newInstance;
    }
}
