using Godot;
using System.Threading.Tasks;

public partial class PlayerAnimation : AnimatedSprite2D
{
	public Vector2 velocity { private get; set; } = Vector2.Zero;

	[Export]
	TopDownAnimation topDownAnimations;

	string state = "Idle";
	string direction = "Down";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		topDownAnimations.changeMoveState += SetState;
		topDownAnimations.changeDirection += SetDirection;
	}

	public override void _ExitTree()
	{
		topDownAnimations.changeMoveState -= SetState;
		topDownAnimations.changeDirection -= SetDirection;
	}

	public override void _PhysicsProcess(double delta)
	{
		topDownAnimations.velocity = velocity;
	}


	private void SetState(string state)
	{
		this.state = state;
		UpdateAnimation();
	}

	private void SetDirection(string direction)
	{
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
