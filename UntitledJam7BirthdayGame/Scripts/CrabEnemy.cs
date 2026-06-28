using Godot;
using System;

public partial class CrabEnemy : RigidBody2D
{
	string state = "Calm";

	[Export]
	AnimatedSprite2D crabAnimation;

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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		targetPosition = Position;

		playerDetection.BodyEntered += DetectionEntered;
		playerDetection.BodyExited += DetectionExited;

		UpdateAnimation();
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

		if (state == "Calm")
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
}
