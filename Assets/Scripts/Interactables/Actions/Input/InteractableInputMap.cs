using System;
using Input;
using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Input
{
	[AddComponentMenu("Interactables/Input/Toggle Input Map")]
	public sealed class InteractableInputMap : Interactable
	{
		[SerializeField] private InputMapEnum target;
		[SerializeField] private bool enable;

		public override void Interact()
		{
			InputManager.InputMap inputMap;
			switch (target)
			{
				case InputMapEnum.Player:
					inputMap = InputManager.Instance.PlayerMap;
					break;
				case InputMapEnum.UI:
					inputMap = InputManager.Instance.UIMap;
					break;
				case InputMapEnum.PersistentGame:
					inputMap = InputManager.Instance.PersistentGameMap;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			inputMap.Enabled = enable;
		}
	}
}