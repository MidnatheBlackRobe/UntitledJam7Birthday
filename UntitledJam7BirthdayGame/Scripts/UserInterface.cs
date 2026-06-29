using Godot;
using System;
using System.ComponentModel;

public partial class UserInterface : CanvasLayer
{
	[Export]
	HealthBar healthBar;

	[Export]
	Control gameOverScreen;

	public void SetHealthBar(float value)
	{
		healthBar.Value = value;
	}

	public void ShowGameOver()
	{
		gameOverScreen.Show();
	}
}
