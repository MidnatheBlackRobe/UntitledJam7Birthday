using Godot;
using System;
using System.ComponentModel;
using System.Threading;

public partial class MainMenu : Control
{
	[Export]
	PackedScene startScene;

	[Export]
	Button startButton;
	[Export]
	Button quitButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton.Pressed += Start;
		quitButton.Pressed += Quit;
	}

	public override void _ExitTree()
    {
        startButton.Pressed -= Start;
		quitButton.Pressed -= Quit;
    }

	private void Start()
	{
		GetTree().ChangeSceneToPacked(startScene);
	}

	private void Quit()
	{
		GetTree().Quit();
	}
}
