using Godot;
using System;

public partial class TitleScreen : MenuContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Paused = false;
		Input.MouseMode = Input.MouseModeEnum.Visible;
		
		base._Ready();
	}
}
