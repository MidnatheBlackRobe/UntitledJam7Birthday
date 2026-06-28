using Godot;
using System;
using System.Threading.Tasks;

public partial class CrabEnemy : RigidBody2D
{
	string state = "Calm";

	[Export]
	AnimatedSprite2D crabAnimation;

	[Export]
	Area2D hitBox;

	[Export]
	Area2D playerDetection;
	[Export]
	RayCast2D playerSight;

	Vector2 targetPosition = Vector2.Zero;
	Player trackedPlayer;
	bool seesPlayer;

	const float TARGET_TOLERANCE = 20f;
	float maxSpeed = 100f;
	float acceleration = 300f;

	float health = 2f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		targetPosition = Position;

		playerDetection.BodyEntered += DetectionEntered;
		playerDetection.BodyExited += DetectionExited;

		hitBox.BodyEntered += OnHit;

		UpdateAnimation();
	}

	public override void _ExitTree()
	{
		playerDetection.BodyEntered -= DetectionEntered;
		playerDetection.BodyExited -= DetectionExited;

		hitBox.AreaEntered -= OnHit;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (trackedPlayer != null) CheckPlayerSight();
		else if (state == "Angry" && Position.DistanceTo(targetPosition) < TARGET_TOLERANCE)
		{
			SetState("Calm");
		}

		if (state == "Angry")
		{
			Vector2 targetDirection = (targetPosition - Position).Normalized();
			LinearVelocity += targetDirection * acceleration * (float)delta;

			LinearVelocity = LinearVelocity.LimitLength(maxSpeed);
		}
	}

	private void SetState(string state)
	{
		if (this.state == state) return;
		this.state = state;
		UpdateAnimation();

		if (state == "Calm" || state == "Death")
		{
			LinearVelocity = Vector2.Zero;
		}
	}

	private void CheckPlayerSight()
	{
		Vector2 playerPosition = trackedPlayer.Position;

		playerSight.LookAt(playerPosition);
		if (!playerSight.IsColliding()) return;

		if (state == "Calm") SetState("Angry");
		targetPosition = playerPosition;
	}

	private void DetectionEntered(Node2D body)
	{
		if (body is Player player) trackedPlayer = player;
	}

	private void DetectionExited(Node2D body)
	{
		if (body != trackedPlayer) return;

		trackedPlayer = null;
	}

	private void UpdateAnimation()
	{
		crabAnimation.Play(state);
	}

	const float DAMAGE_KNOCKBACK = 200f;
	const float DAMAGE_TIME = 0.25f;

	public async Task Damage(Vector2 position)
	{
		SetState("Hit");
		health -= 1f;
		Vector2 knockback = (Position - position).Normalized() * DAMAGE_KNOCKBACK;
		LinearVelocity += knockback;
		if (health <= 0)
		{
			_ = OnDeath();
		}
		await ToSignal(GetTree().CreateTimer(DAMAGE_TIME), SceneTreeTimer.SignalName.Timeout);
		SetState("Angry");
	}

	Action crabDied;
	const float DEATH_TIME = 0.85f;

	public async Task OnDeath()
	{
		hitBox.ProcessMode = ProcessModeEnum.Disabled;
		crabDied?.Invoke();
		SetState("Death");
		await ToSignal(GetTree().CreateTimer(DEATH_TIME), SceneTreeTimer.SignalName.Timeout);
		Hide();
		ProcessMode = ProcessModeEnum.Disabled;
	}

	private void OnHit(Node2D body)
	{
		GD.Print("Hit");
		if (body is Player player)
		{
			GD.Print("HitPlayer");
			player.Damage(Position);
		}
	}
}
