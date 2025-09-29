using Managers;

public static class Utilities
{
	public static void ForcePlayerInput(this InputManager inputManager)
	{
		inputManager.ToggleUIMap(false);
		inputManager.TogglePlayerMap(true);
	}
	
	public static void ForceUIInput(this InputManager inputManager)
	{
		inputManager.TogglePlayerMap(false);
		inputManager.ToggleUIMap(true);
	}
}