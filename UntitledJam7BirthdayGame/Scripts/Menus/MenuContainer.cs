using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class MenuContainer : Control
{
    private Array<ManagedMenu> managedMenus = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        foreach (Node node in GetChildren())
        {
            if (node is ManagedMenu managedMenu)
            {
                managedMenus.Add(managedMenu);
            }
        }

        foreach (ManagedMenu managedMenu in managedMenus)
        {
            managedMenu.SwitchMenu += SwitchMenu;  
        }
	}

	public override void _ExitTree()
    {
        foreach (ManagedMenu managedMenu in managedMenus)
        {
            managedMenu.SwitchMenu -= SwitchMenu;  
        }
    }

	private void SwitchMenu(Control currentMenu, string newMenuName)
	{
        Control newMenu = GetNode<Control>(newMenuName);
        
        if (newMenu != null)
        {
            newMenu.Visible = true;
            newMenu.ProcessMode = ProcessModeEnum.Inherit;

            currentMenu.Visible = false;
            currentMenu.ProcessMode = ProcessModeEnum.Disabled;
        }
	}
}
