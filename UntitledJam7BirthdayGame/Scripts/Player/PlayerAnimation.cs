using Godot;

public partial class PlayerAnimation : AnimatedSprite2D
{
	public void UpdateAnimation(string state, string direction)
	{
		if (state == "Death")
		{
			Play(state);
			return;
		}
		if (direction is "Left" or "Right")
		{
			FlipH = direction == "Left";
			direction = "Side";
		}
		Play(direction + "_" + state);
	}
}
