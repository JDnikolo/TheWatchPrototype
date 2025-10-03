using Managers;
using UnityEngine;
using Utilities;

namespace UI
{
	[AddComponentMenu("UI/Start With UI Input")]
	public sealed class StartWithUi : MonoBehaviour, IStartable
	{
		public byte StartOrder => byte.MaxValue;

		public void OnStart() => InputManager.Instance.ForceUIInput();
	}
}