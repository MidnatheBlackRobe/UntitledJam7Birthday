using System;
using System.Data.Common;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	PlayerAnimation playerAnimation;
	[Export]
	PlayerWeapon playerWeapon;
	[Export]
	PlayerHurtBox playerHurtBox;

	string direction = "Down";
	string state = "Idle";

	public override void _Ready()
	{
		playerHurtBox.Damaged += Damage;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		PlayerMovement(delta);
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("attack") && state is not "Attack" or "Hit" or "Death")
		{
			_ = Attack();
		}
	}

	float maxSpeed = 150f;
	float acceleration = 450f;
	float brakingFriction = 1200f;

	Vector2 lastMoveInput = Vector2.Zero;

	private void PlayerMovement(double delta)
	{
		if (state is "Attack" or "Hit" or "Death") return;

		Vector2 moveInput = Input.GetVector("left", "right", "up", "down"); ;

		if (moveInput == Vector2.Zero)
		{
			SetState("Idle");
			if (Velocity == Vector2.Zero) return;
			if (Velocity.Length() <= brakingFriction * (float)delta)
			{
				Velocity = Vector2.Zero;
				return;
			}
		}

		SetState("Walk");
		Velocity += (moveInput - Velocity.Normalized()) * brakingFriction * (float)delta;

		Velocity += moveInput * acceleration * (float)delta;

		Velocity = Velocity.LimitLength(maxSpeed);

		if (Mathf.Abs(Velocity.X) > Mathf.Abs(Velocity.Y))
		{
			direction = Velocity.X <= 0 ? "Left" : "Right";
		}
		else
		{
			direction = Velocity.Y <= 0 ? "Up" : "Down";
		}

		lastMoveInput = moveInput;
	}

	private const float MAX_HEALTH = 1f;
	private const float HEALTH_STEP = 0.25f;
	private const float DAMAGE_KNOCKBACK = 200f;
	private const float DAMAGE_TIME = 0.25f;
	private float health = MAX_HEALTH;
	public Action<float> healthChanged;

	private void Damage(Vector2 position)
	{
		_ = DamageRoutine(position);
	}

	private async Task DamageRoutine(Vector2 position)
	{
		health -= HEALTH_STEP;
		healthChanged?.Invoke(health);
		if (health <= 0)
		{
			_ = OnDeath();
			return;
		}
		SetState("Hit");
		Vector2 knockback = (Position - position).Normalized() * DAMAGE_KNOCKBACK;
		Velocity = knockback;
		await ToSignal(GetTree().CreateTimer(DAMAGE_TIME), SceneTreeTimer.SignalName.Timeout);
		SetState("Idle");
	}

	private const float DEATH_TIME = 1.28f;
	public Action playerDied;

	public async Task OnDeath()
	{
		SetState("Death");
		await ToSignal(GetTree().CreateTimer(DEATH_TIME), SceneTreeTimer.SignalName.Timeout);
		playerDied?.Invoke();
		Hide();
		ProcessMode = ProcessModeEnum.Disabled;
	}

	public async Task Attack()
	{
		SetState("Attack");
		await playerWeapon.Attack(direction);
		SetState("Idle");
	}

	private void SetState(string state)
	{
		this.state = state;
		playerAnimation.UpdateAnimation(state, direction);
		if (state == "Attack")
		{
			Velocity = Vector2.Zero;
		}
	}

	private void SetDirection(string direction)
	{
		this.direction = direction;
		playerAnimation.UpdateAnimation(state, direction);
	}
}
