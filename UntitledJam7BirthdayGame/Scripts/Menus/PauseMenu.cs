using Godot;

public partial class PauseMenu : ManagedMenu
{
    [Export]
    string settingsMenuName = "SettingsMenu";
	[Export]
	Button SettingsButton;
	[Export]
	Button ResumeButton;

	[Signal]
    public delegate void ResumeEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SettingsButton.Pressed += SwitchToSettings;
		ResumeButton.Pressed += EmitResume;
	}

    public override void _ExitTree()
    {
        SettingsButton.Pressed -= SwitchToSettings;
		ResumeButton.Pressed -= EmitResume;
    }

	private void EmitResume()
	{
		EmitSignal(SignalName.Resume);
	}

	private void SwitchToSettings()
	{
		EmitSwitchMenu(this, settingsMenuName);
	}
}
