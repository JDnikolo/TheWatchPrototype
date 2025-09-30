using UnityEngine;

namespace Interactables
{
	public interface IHighlightableInteractable : IInteractable, IHighlightable
	{
		Vector3 Position { get; }
	}
}