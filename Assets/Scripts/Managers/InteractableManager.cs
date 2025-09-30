using System.Collections.Generic;
using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
	public sealed class InteractableManager : Singleton<InteractableManager>, IStartable
	{
		[SerializeField] private string interactActionName = "Interact";
		
		private readonly HashSet<IHighlightableInteractable> m_interactables = new();
		private IHighlightableInteractable m_interactable;
		private InputAction m_interactAction;
		
		public byte StartOrder => 0;

		public void OnStart() => m_interactAction = InputManager.Instance.GetPlayerAction(interactActionName);

		private void Update()
		{
			UpdateInteractable();
			if (m_interactable == null || !m_interactAction.WasPressedThisFrame()) return;
			m_interactable.Interact();
		}

		private void UpdateInteractable()
		{
			if (m_interactables.Count < 1) return;
			var dot = 0.99f;
			IHighlightableInteractable target = null;
			var cameraTransform = PlayerManager.Instance.Camera.transform;
			var cameraPosition = cameraTransform.position;
			var cameraForward = cameraTransform.forward;
			foreach (var interactable in m_interactables)
			{
				var tmVector = Vector3.Normalize(interactable.Position - cameraPosition);
				var newDot = Vector3.Dot(tmVector, cameraForward);
				if (newDot > dot)
				{
					dot = newDot;
					target = interactable;
				}
			}

			//The interactable did not change so nothing happens
			if (target == m_interactable) return;
			//The old interactable gets un-highlighted, if there is one
			if (m_interactable != null) m_interactable.Highlight(false);
			m_interactable = target;
			//The new interactable gets highlighted, if there is one
			if (target != null) target.Highlight(true);
		}
		
		public void AddInteractable(IHighlightableInteractable interactable)
		{
			if (interactable == null) return;
			m_interactables.Add(interactable);
		}

		public void RemoveInteractable(IHighlightableInteractable interactable)
		{
			if (interactable == null) return;
			m_interactables.Remove(interactable);
			if (m_interactable == interactable) m_interactable = null;
			interactable.Highlight(false);
		}
	}
}