using Managers;
using UnityEngine;
using Utilities;

namespace Player
{
	[AddComponentMenu(menuName: "Player/Start With Player Input")]
	public sealed class StartWithPlayer : MonoBehaviour, IStartable
	{
		public byte StartOrder => byte.MaxValue;

		public void OnStart() => InputManager.Instance.ForcePlayerInput();
	}
}