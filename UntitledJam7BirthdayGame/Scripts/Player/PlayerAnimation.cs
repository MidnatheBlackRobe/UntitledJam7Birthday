using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayerAnimation : AnimatedSprite2D
{
	public Vector2 Velocity { private get; set; } = Vector2.Zero;

	private string direction = "Down";

	[Export]
	TopDownAnimation topDownAnimations;

	string state = "Idle";

	Action<string> changeDirection;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		topDownAnimations.changeMoveState += SetState;
	}

	public override void _ExitTree()
	{
		topDownAnimations.changeMoveState -= SetState;
	}

	public override void _PhysicsProcess(double delta)
	{
		topDownAnimations.velocity = Velocity;
	}


	private void SetState(string state)
	{
		this.state = state;
		UpdateAnimation();
	}

	public void SetDirection(string direction)
	{
		changeDirection?.Invoke(direction);
		if (direction is "Left" or "Right")
		{
			this.direction = "Side";
			FlipH = direction == "Left";
		}
		else this.direction = direction;
		UpdateAnimation();
	}

	private void UpdateAnimation()
	{
		Play(direction + "_" + state);
	}

	public async Task Attack()
	{
		topDownAnimations.ProcessMode = ProcessModeEnum.Disabled;
		SetState("Attack");
		await ToSignal(this, AnimatedSprite2D.SignalName.AnimationFinished);
		topDownAnimations.ProcessMode = ProcessModeEnum.Inherit;
	}
}
