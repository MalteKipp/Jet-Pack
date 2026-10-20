using Godot;
using System;



public partial class Character : CharacterBody2D
{
    float gravity = 800;
    float flySpeed = 1600;
	[Signal]
	public delegate void DeathEventHandler();
    [Signal]
	public delegate void ScoreEventHandler();


    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

       
        velocity.Y += gravity*(float)delta;

        if (Input.IsActionPressed("Fly"))
        {
            velocity.Y -= flySpeed*(float)delta;
        }

        Velocity = velocity;

        MoveAndSlide();
    }

	public override void _Process(double delta)
	{
	}


	private void OnBodyEntered(Node2D body)
    {
        if (body is Plasma)
        {
                
                EmitSignal(SignalName.Death);
        }
        else if (body is Rocket)
        {
            EmitSignal(SignalName.Death);
        }

    }

    public void OnAreaEntered(Area2D area){
        if (area.GetParent() is Coin coin)
        {
            //GD.Print("hello");
			EmitSignal(SignalName.Score);
            coin.CallDeferred(Node.MethodName.QueueFree);
        }
    }

}
