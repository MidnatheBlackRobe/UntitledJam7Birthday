using Godot;
using System;

public partial class HealthBar : TextureProgressBar
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Value = MaxValue;
	}

	public void Reset()
	{
		Value = MaxValue;
	}

	public void Damage()
	{
		Value -= 1;
		if (Value <= 0) Value = MaxValue;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			Damage();
		}
	}
}
