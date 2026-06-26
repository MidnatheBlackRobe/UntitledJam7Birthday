using Godot;

public partial class VolumeSlider : HSlider
{
	[Export]
	private int audioBusID;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(audioBusID));

		ValueChanged += ChangeVolume;
	}

    public override void _ExitTree()
	{
		ValueChanged -= ChangeVolume;
	}

	private void ChangeAudioValue(int busID, double linearValue)
	{
		AudioServer.SetBusVolumeDb(busID, Mathf.LinearToDb((float)linearValue));
	}

	private void ChangeVolume(double value)
	{
        AudioServer.SetBusVolumeDb(audioBusID, Mathf.LinearToDb((float)value));
	}
}
