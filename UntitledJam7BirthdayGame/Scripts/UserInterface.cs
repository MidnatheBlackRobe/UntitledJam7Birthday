using Godot;
using System;
using System.ComponentModel;

public partial class UserInterface : CanvasLayer
{
	[Export]
	HealthBar healthBar;

	public void SetHealthBar(float value)
	{
		healthBar.Value = value;
	}
}
