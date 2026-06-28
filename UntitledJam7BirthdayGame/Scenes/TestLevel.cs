using Godot;
using System;

public partial class TestLevel : Node2D
{
	[Export]
	Player player;

	[Export]
	UserInterface userInterface;

	Action<float> SetHealthBar => userInterface.SetHealthBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player.healthChanged += SetHealthBar;
	}

	public override void _ExitTree()
	{
		player.healthChanged -= SetHealthBar;
	}

}
