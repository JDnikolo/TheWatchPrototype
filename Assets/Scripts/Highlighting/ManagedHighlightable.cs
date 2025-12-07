using System;
using LookupTables;
using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Managed Highlightable")]
	public sealed class ManagedHighlightable : Highlightable, IManagedHighlightable
	{
		[SerializeField] private Highlightable highlightable;
		//TODO Replace this with a rigidbody
		[SerializeField] private new Collider collider;
		[SerializeField] private float minHighlightDistance;
		[SerializeField] private float maxHighlightDistance = 3f;

		public float MinHighlightDistance => minHighlightDistance;

		public float MaxHighlightDistance  => maxHighlightDistance;

		private void Start()
		{
			if (!collider) throw new Exception("Collider required for highlighting.");
			RigidBodyTable.Instance.Add(collider.attachedRigidbody, this);
		}

		protected override void HighlightInternal(bool enabled)
		{
			if (highlightable) highlightable.Highlight(enabled);
		}
	}
}