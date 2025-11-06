/*
using Interactables;

namespace UI.Elements
{
	public sealed partial class Button
	{
		private struct ButtonInteractions : IInteractable
		{
			private Interactable[] m_interactables;

			public ButtonInteractions(Interactable[] interactables) => m_interactables = interactables;

			public void Interact()
			{
				if (m_interactables == null) return;
				var length  = m_interactables.Length;
				if (length == 0) return;
				for (var i = 0; i < length; i++)
				{
					var interactable = m_interactables[i];
					if (interactable) interactable.Interact();
				}
			}
		}
	}
}
*/