using Godot;

public abstract partial class ManagedMenu : Control
{
    [Signal]
    public delegate void SwitchMenuEventHandler(Control currentMenu, string newMenuName);

    protected void EmitSwitchMenu(Control currentMenu, string newMenuName)
    {
        EmitSignal(SignalName.SwitchMenu, currentMenu, newMenuName);
    }
}