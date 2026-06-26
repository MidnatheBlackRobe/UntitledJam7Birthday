using Godot;

public partial class SwitchSceneButton : Button
{
	[Export(PropertyHint.File, "*.tscn")]
    public string ScenePath { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += SwitchScene;
	}

	public override void _ExitTree()
    {
        Pressed -= SwitchScene;
    }

	private void SwitchScene()
	{
		PackedScene scene = GD.Load<PackedScene>(ScenePath);
		GetTree().ChangeSceneToPacked(scene);
	}
}
