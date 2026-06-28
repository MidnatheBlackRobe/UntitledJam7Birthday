using System;
using Godot;

public partial class TopDownAnimation : Node2D
{
	public Vector2 velocity { private get; set; } = Vector2.Zero;

	public Action<string> changeMoveState;
	public Action<string> changeDirection;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (velocity == Vector2.Zero)
		{
			changeMoveState?.Invoke("Idle");
			return;
		}

		changeMoveState?.Invoke("Walk");

		if (Mathf.Abs(velocity.X) > Mathf.Abs(velocity.Y))
		{
			changeDirection?.Invoke(velocity.X <= 0 ? "Left" : "Right");
		}
		else
		{
			changeDirection?.Invoke(velocity.Y <= 0 ? "Up" : "Down");
		}
	}
}
