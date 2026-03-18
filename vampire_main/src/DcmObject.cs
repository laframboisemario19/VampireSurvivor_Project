using System;
using System.Collections;
using Godot;
using Utils;
using static MedAttack;

public partial class DcmObject : Node2D, ISpawn, ICollide
{
    [ExportGroup("External")]
    [Export]
    private PackedScene TreasureScene,
        GemScene,
        TrapScene;

    [Export]
    private MedAttack MediateurAttack;

    [ExportGroup("Internal")]
    [Export]
    private Timer timer1,
        timer2;

    private ArrayList TimerList = [];
    private ArrayList SceneList = [];

    public void Collide(
        EAlgoSelectionDetection InAlgoSelectionDetection,
        Node2D InEntering,
        Node2D InEntered
    )
    {
        MediateurAttack.Collide(InAlgoSelectionDetection, InEntering, InEntered);
    }

    public override void _Ready()
    {
        base._Ready();
        TimerList.AddRange(new[] { timer1, timer2 });
        SceneList.AddRange(new[] { TreasureScene, TrapScene });
        _ActivateObject();
    }

    private void _ActivateObject()
    {
        foreach (Timer timer in TimerList)
        {
            timer.EnsureValid().Timeout += () =>
            {
                int index = TimerList.IndexOf(timer);
                BaseObject spawn = (BaseObject)((ISpawn)this).Spawn((PackedScene)SceneList[index]);
                spawn.GlobalPosition = _DefinePosition();
                AddChild(spawn);
            };
        }
    }

    public void SpawnGem(Vector2 InPosition)
    {
        BaseObject spawn = (BaseObject)((ISpawn)this).Spawn(GemScene);
        spawn.GlobalPosition = InPosition;
        AddChild(spawn);
    }

    private Vector2 _DefinePosition()
    {
        float tailleX = 1000.0f;
        float tailleY = 600.0f;
        float decalageX = 900.0f;
        float decalageY = 500.0f;
        int coteApparition = GD.RandRange(0, 3);
        Vector2 v = new(0, 0);

        switch (coteApparition)
        {
            case 0:
                v = new((float)GD.RandRange(-tailleX, tailleX), -decalageY);
                break;
            case 1:
                v = new(decalageX, (float)GD.RandRange(-tailleY, tailleY));
                break;
            case 2:
                v = new((float)GD.RandRange(-tailleX, tailleX), decalageY);
                break;
            case 3:
                v = new(decalageX, (float)GD.RandRange(-tailleY, tailleY));
                break;
        }

        return v;
    }
}
