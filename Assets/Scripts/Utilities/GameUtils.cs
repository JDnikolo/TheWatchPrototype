using System;
using Managers;

namespace Utilities
{
	public static partial class Utils
	{
		public static void InvokeOnNextFrameUpdateSafe(this GameManager gameManager, Action action)
		{
			if (gameManager) gameManager.InvokeOnNextFrameUpdate(action);
		}
		
		public static void AddFrameUpdateSafe(this GameManager gameManager, IFrameUpdatable updatable)
		{
			if (gameManager) gameManager.AddFrameUpdate(updatable);
		}

		public static void RemoveFrameUpdateSafe(this GameManager gameManager, IFrameUpdatable updatable)
		{
			if (gameManager) gameManager.RemoveFrameUpdate(updatable);
		}

		public static void InvokeOnNextFixedUpdateSafe(this GameManager gameManager, Action action)
		{
			if (gameManager) gameManager.InvokeOnNextFixedUpdate(action);
		}
		
		public static void AddFixedUpdateSafe(this GameManager gameManager, IFixedUpdatable updatable)
		{
			if (gameManager) gameManager.AddFixedUpdate(updatable);
		}

		public static void RemoveFixedUpdateSafe(this GameManager gameManager, IFixedUpdatable updatable)
		{
			if (gameManager) gameManager.RemoveFixedUpdate(updatable);
		}
	}
}