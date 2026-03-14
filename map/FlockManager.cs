using Godot;
using System;

public partial class FlockManager : Node
{

    private int dice;
    private int randomSlop;
    
    public override void _Ready() {
        base._Ready();

        var rng = new RandomNumberGenerator();
        rng.Randomize();

        foreach (AnimatedSprite2D oiseau in this.GetChildren())
        {
            Vector2 newPositionX = oiseau.GlobalPosition;
            newPositionX.X += 800;
            oiseau.GlobalPosition = newPositionX;

            float randomYPosition = rng.RandfRange(-300.0f,300.0f);
            Vector2 newPositionY = oiseau.GlobalPosition;
            newPositionY.Y += randomYPosition;
            oiseau.GlobalPosition = newPositionY;

            dice = rng.RandiRange(1,2);   
            randomSlop = rng.RandiRange(5,10);
        }

    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        var rng = new RandomNumberGenerator();
        rng.Randomize();     
        
        foreach (AnimatedSprite2D oiseau in this.GetChildren())
        {

            Vector2 oiseauPosition = oiseau.GlobalPosition;

            float randomSpeed = rng.RandfRange(50.0f,100.0f);

            oiseauPosition.X -= randomSpeed * (float)delta;
            if (dice == 1)
            {
                oiseauPosition.Y -= (randomSpeed / randomSlop) * (float)delta;
            }
            else
            {
                oiseauPosition.Y += (randomSpeed / randomSlop) * (float)delta;
            }

            if (oiseauPosition.Y < -450 || oiseauPosition.Y > 450 || oiseauPosition.X < -810)
            {
                float randomYPosition = rng.RandfRange(-300.0f,300.0f);
                oiseauPosition.Y = randomYPosition;
                oiseauPosition.X = 800;
                dice = rng.RandiRange(1,2); 
                randomSlop = rng.RandiRange(5,10);
            }

            oiseau.GlobalPosition = oiseauPosition;
            
        }




    }


}
