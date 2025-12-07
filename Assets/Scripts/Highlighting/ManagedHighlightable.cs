using LookupTables;
using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Managed Highlightable")]
	public sealed class ManagedHighlightable : Highlightable, IManagedHighlightable
	{
		[SerializeField] private Highlightable highlightable;
		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private float minHighlightDistance;
		[SerializeField] private float maxHighlightDistance = 3f;

		public float MinHighlightDistance => minHighlightDistance;

		public float MaxHighlightDistance  => maxHighlightDistance;

		private void Start() => RigidBodyTable.Instance.Add(rigidbody, this);

		protected override void HighlightInternal(bool enabled)
		{
			if (highlightable) highlightable.Highlight(enabled);
		}
	}
}