using Managers;
using UnityEngine;

namespace Player
{
	public sealed class StartWithPlayer : MonoBehaviour, IStartable
	{
		public byte StartOrder => byte.MaxValue;

		public void OnStart() => InputManager.Instance.ForcePlayerInput();
	}
}