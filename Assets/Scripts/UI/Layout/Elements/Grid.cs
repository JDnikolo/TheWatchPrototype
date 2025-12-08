using System.Collections.Generic;
using Callbacks.Layout;
using UnityEngine;
using Utilities;
using Node = System.Collections.Generic.LinkedListNode<UI.Layout.ILayoutElement>;
using Column = System.Collections.Generic.LinkedList<UI.Layout.ILayoutElement>;
using Row = System.Collections.Generic.LinkedListNode<System.Collections.Generic.LinkedList<UI.Layout.ILayoutElement>>;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Grid")]
	public sealed class Grid : Element, ILayoutControllingParent, ILayoutExplicit, ILayoutExplicitCallbackImplementer
	{
		[SerializeField] private bool selectable;
		[SerializeField] private bool autoReset;

		private readonly LinkedList<Column> m_elements = new();
		private ILayoutExplicitCallback m_explicitCallback;
		private Node m_selectedNode;

		public bool IsExplicit => selectable;
		
		public ILayoutElement FirstChild => selectable ? null : m_selectedNode?.Value;

		public void SetExplicitCallback(ILayoutExplicitCallback callback) => m_explicitCallback = callback;

		public void SelectExplicit() => m_explicitCallback?.OnSelectedExplicit();

		public void DeselectExplicit() => m_explicitCallback?.OnDeselectedExplicit();

		public void OnMissedInput(Vector2 axis, Direction input) => OnInput(axis, input);
		
		public ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input)
		{
			if (oldElement == null) return null;
			ILayoutElement element = this;
			if (oldElement.Parent != element) return null;
			var lastNode = Find(oldElement, DirectionFlags.Down | DirectionFlags.Right);
			if (lastNode == null) return null;
			m_selectedNode = lastNode;
			if (selectable) return this;
			return FirstChild;
		}

		public void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement)
		{
			throw new System.NotImplementedException();
		}

		private Node Find(ILayoutElement element, DirectionFlags direction)
		{
			throw new System.NotImplementedException();
		}
		
		private bool IsChildElement(ILayoutElement element) => element != null && ReferenceEquals(element.Parent, this);
		
		private void SetFirstNode()
		{
		}
		
		public override void OnPrewarm()
		{
			base.OnPrewarm();
			//this.CollectChildren<LayoutElement, ILayoutElement>(m_elements);
			SetFirstNode();
		}
#if UNITY_EDITOR
		public bool Selectable
		{
			get => selectable;
			set => this.DirtyReplaceValue(ref selectable, value);
		}
		
		public Node SelectedNode => m_selectedNode;
		
		public DirectionFlags BlockedDirections => DirectionFlags.All;
		
		protected override void OnValidate()
		{
			base.OnValidate();
			//SetElementParents();
		}
#endif
	}
}