using Godot;
using System;

public partial class PlayerHurtBox : Area2D
{
	public Action<Vector2> Damaged;

	public void Damage(Vector2 position)
	{
		Damaged?.Invoke(position);
	}

}
