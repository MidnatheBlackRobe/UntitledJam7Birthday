using Godot;
using System;
using System.Threading.Tasks;

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
		player.playerDied += GameOver;
	}

	public override void _ExitTree()
	{
		player.healthChanged -= SetHealthBar;
		player.playerDied -= GameOver;
	}

	private void GameOver()
	{
		_ = GameOverRoutine();

	}

	private const float GAME_OVER_TIME = 2f;

	private async Task GameOverRoutine()
	{
		userInterface.ShowGameOver();
		await ToSignal(GetTree().CreateTimer(GAME_OVER_TIME), SceneTreeTimer.SignalName.Timeout);
		GetTree().ReloadCurrentScene();
	}
}
