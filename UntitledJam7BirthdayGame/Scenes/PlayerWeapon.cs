using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayerWeapon : Area2D
{
	[Export]
	CollisionShape2D leftHitBox;
	[Export]
	CollisionShape2D upHitBox;
	[Export]
	CollisionShape2D rightHitBox;
	[Export]
	CollisionShape2D downHitBox;

	private const float ATTACK_TIME = 0.5f;

	public override void _Ready()
	{
		BodyEntered += OnHit;
	}

	public override void _ExitTree()
	{
		BodyEntered -= OnHit;
	}


	public async Task Attack(string direction)
	{
		CollisionShape2D hitBox = leftHitBox;
		if (direction == "Up") hitBox = upHitBox;
		else if (direction == "Right") hitBox = rightHitBox;
		else if (direction == "Down") hitBox = downHitBox;

		hitBox.Disabled = false;
		GD.Print("Attacked");

		await ToSignal(GetTree().CreateTimer(ATTACK_TIME), SceneTreeTimer.SignalName.Timeout);

		hitBox.Disabled = true;
	}

	private void OnHit(Node2D hit)
	{
		if (hit is CrabEnemy crabEnemy)
		{
			crabEnemy.Damage();
		}
	}
}
