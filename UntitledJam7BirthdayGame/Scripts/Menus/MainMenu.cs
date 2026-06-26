using Godot;

public partial class MainMenu : ManagedMenu
{
    [Export]
    string settingsMenuName = "SettingsMenu";
	[Export]
	Button SettingsButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SettingsButton.Pressed += SwitchToSettings;
	}

    public override void _ExitTree()
    {
        SettingsButton.Pressed -= SwitchToSettings;
    }

	private void SwitchToSettings()
	{
		EmitSwitchMenu(this, settingsMenuName);
	}
}
