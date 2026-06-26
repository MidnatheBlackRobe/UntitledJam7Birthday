using Godot;

public partial class QuitButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += Quit;
	}

	public override void _ExitTree()
    {
        Pressed -= Quit;
    }

	private void Quit()
	{
		GetTree().Quit();
	}
}
