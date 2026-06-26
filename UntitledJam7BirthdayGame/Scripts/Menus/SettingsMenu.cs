using Godot;

public partial class SettingsMenu : ManagedMenu
{
	[Export]
	string backMenuName = "MainMenu";
	[Export]
	Button BackButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BackButton.Pressed += SwitchToMain;
	}

    public override void _ExitTree()
    {
        BackButton.Pressed -= SwitchToMain;
    }

	private void SwitchToMain()
	{
		EmitSwitchMenu(this, backMenuName);
	}
}
