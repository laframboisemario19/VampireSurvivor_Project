using System;
using Godot;

public partial class BaseEnemy : Node2D
{
    [ExportGroup("Internal")]
    [Export]
    protected EnemyArea Area;

    [Export]
    protected Poursuite Poursuite;

    [Export]
    protected AnimatedSprite2D AnimatedSprite;
    public bool isDying = false;

    // public void TakeDamage()
    // {
    //     if (!isDying)
    //     {
    //         isDying = true;
    //         var anim = Area.GetParent().GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    //         string currentAnimName = anim.Animation;

    //         switch (currentAnimName)
    //         {
    //             case "marche_bas":
    //                 v = new((float)GD.RandRange(-_tailleX, _tailleX), -_decalageY);
    //                 break;
    //             case "marche_haut":
    //                 v = new(_decalageX, (float)GD.RandRange(-_decalageY, _decalageY));
    //                 break;
    //             case "marche_cote":
    //                 v = new((float)GD.RandRange(-_tailleX, _tailleX), _decalageY);
    //                 break;
    //             case 3:
    //                 v = new(_decalageX, (float)GD.RandRange(-_decalageY, _decalageY));
    //                 break;
    //         }

    //         Area.GlobalPosition.Area.Die();
    //         Poursuite.Velocity = 60.0f;
    //         AnimatedSprite.Play("explode");
    //         AnimatedSprite.AnimationFinished += () => QueueFree();
    //     }
    // }

    public void Die()
    {
        if (!isDying)
        {
            isDying = true;
            Area.Die();
            Poursuite.Velocity = 60.0f;
            AnimatedSprite.Play("explode");
            AnimatedSprite.AnimationFinished += () => QueueFree();
        }
    }
}
