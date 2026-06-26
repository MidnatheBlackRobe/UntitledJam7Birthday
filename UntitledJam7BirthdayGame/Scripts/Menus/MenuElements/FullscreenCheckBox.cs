using Godot;

public partial class FullscreenCheckBox : CheckBox
{
    public override void _Ready()
    {
        bool isFullscreen = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen;
        ButtonPressed = isFullscreen;

        Toggled += OnFullscreenToggled;
    }

    private void OnFullscreenToggled(bool toggledOn)
    {
        if (toggledOn)
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        else
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
    }
}