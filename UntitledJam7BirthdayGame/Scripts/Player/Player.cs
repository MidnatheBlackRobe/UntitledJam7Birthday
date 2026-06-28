using System.Data.Common;
using System.Runtime.Serialization.Formatters;
using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	PlayerAnimation playerAnimation;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		PlayerMovement(delta);
		playerAnimation.velocity = Velocity;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			_ = playerAnimation.Attack();
			GD.Print("Hi");
		}
	}

	float maxSpeed = 150f;
	float acceleration = 450f;
	float brakingFriction = 1200f;

	Vector2 lastMoveInput = Vector2.Zero;

	private void PlayerMovement(double delta)
	{
		Vector2 moveInput = Input.GetVector("left", "right", "up", "down"); ;

		if (moveInput == Vector2.Zero)
		{
			if (Velocity == Vector2.Zero) return;
			if (Velocity.Length() <= brakingFriction * (float)delta)
			{
				Velocity = Vector2.Zero;
				MoveAndSlide();
				return;
			}
		}

		Velocity += (moveInput - Velocity.Normalized()) * brakingFriction * (float)delta;

		Velocity += moveInput * acceleration * (float)delta;

		Velocity = Velocity.LimitLength(maxSpeed);

		MoveAndSlide();

		lastMoveInput = moveInput;
	}
}
