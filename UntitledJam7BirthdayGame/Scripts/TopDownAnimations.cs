using Godot;

public partial class TopDownAnimations : AnimatedSprite2D
{
	CharacterBody2D characterBody2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		characterBody2D = GetParent<CharacterBody2D>();
		if (characterBody2D == null)
		{
			GD.PushError("Parent must be a CharacterBody2D");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = characterBody2D.Velocity;

		if (velocity == Vector2.Zero)
		{
			Stop();
			return;
		}

		if (Mathf.Abs(velocity.X) > Mathf.Abs(velocity.Y))
        {
            Play("Side_Idle");
			FlipH = velocity.X < 0;
        } else
		{
			Play(velocity.Y >= 0 ? "Down_Idle" : "Up_Idle");
		}
	}
}
