using Godot;
using System;

public partial class PausePanel : MenuContainer
{
	[Export]
	PauseMenu pauseMenu;

    public override void _Ready()
    {
		pauseMenu.Resume += Resume;
		base._Ready();
    }

    public override void _ExitTree()
    {
        pauseMenu.Resume -= Resume;
		base._ExitTree();
    }


    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            TogglePause();
        }
    }

	private void Resume()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        bool isPaused = !GetTree().Paused;
        GetTree().Paused = isPaused;

        Visible = isPaused;
    }
}