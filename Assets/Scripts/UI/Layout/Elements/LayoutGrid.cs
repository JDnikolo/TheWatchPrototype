using System;
using System.Collections.Generic;
using System.Linq;
using Callbacks.Layout;
using UnityEngine;
using Utilities;
using ListGrid = System.Collections.Generic.List<System.Collections.Generic.List<UI.Layout.ILayoutElement>>;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Grid")]
	public sealed class LayoutGrid : LayoutElement, ILayoutControllingParent, ILayoutExplicit, ILayoutExplicitCallbackImplementer
	{
		[SerializeField] private Axis primaryAxis;
		[SerializeField] private bool selectable;
		[SerializeField] private bool autoReset;

		private readonly HashSet<ILayoutElement> m_elements = new();
		private ILayoutExplicitCallback m_explicitCallback;
		private ILayoutElement m_selectedElement;

		public bool IsExplicit => selectable;
		
		public ILayoutElement FirstChild => selectable ? null : m_selectedElement;

		public void SetExplicitCallback(ILayoutExplicitCallback callback) => m_explicitCallback = callback;

		public void SelectExplicit() => m_explicitCallback?.OnSelectedExplicit();

		public void DeselectExplicit() => m_explicitCallback?.OnDeselectedExplicit();

		public void OnMissedInput(Vector2 axis, Direction input) => OnInput(axis, input);
		
		public ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input)
		{
			if (oldElement == null) return null;
			ILayoutElement element = this;
			if (oldElement.Parent != element || !m_elements.Contains(element)) return null;
			m_selectedElement = element;
			if (selectable) return this;
			return FirstChild;
		}

		public void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement)
		{
			if (autoReset) m_selectedElement = null;
			else if (m_elements.Contains(oldElement)) m_selectedElement = oldElement;
			if (m_selectedElement == null) SetFirstNode();
		}
		
		private void SetFirstNode() => m_selectedElement = m_elements.FirstOrDefault();

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			var parent = transform;
			var elementsLength = parent.childCount;
			for (var i = 0; i < elementsLength; i++)
			{
				if (!parent.GetChild(i).TryGetComponent(out LayoutGridChild gridChild)) continue;
				var elements = gridChild.Elements;
				for (var j = 0; j < elements.Length; j++)
				{
					var element = elements[j];
					if (!element) continue;
					m_elements.Add(element);
				}
			}
			
			SetFirstNode();
		}
		
#if UNITY_EDITOR
		public bool Selectable
		{
			get => selectable;
			set => this.DirtyReplaceValue(ref selectable, value);
		}
		
		public HashSet<ILayoutElement> Elements => m_elements;
		
		public ILayoutElement SelectedElement => m_selectedElement;
		
		public DirectionFlags BlockedDirections => DirectionFlags.All;
		
		protected override void OnValidate()
		{
			base.OnValidate();
			SetElementParents();
		}

		private void SetElementParents()
		{
			var elements = new ListGrid();
			List<ILayoutElement> list;
			var parent = transform;
			var elementsLength = parent.childCount;
			int y;
			for (y = 0; y < elementsLength; y++)
			{
				var child = parent.GetChild(y);
				if (!child.TryGetComponent(out LayoutGridChild gridChild)) continue;
				list = new List<ILayoutElement>(gridChild.Elements);
				elements.Add(list);
			}

			elementsLength = elements.Count;
			int x;
			switch (primaryAxis)
			{
				case Axis.Horizontal:
					for (x = 0; x < elementsLength; x++)
					{
						list = elements[x];
						var listLength = list.Count;
						for (y = 0; y < listLength; y++) LinkElement(elements, x, y);
					}
					
					break;
				case Axis.Vertical:
					for (y = 0; y < elementsLength; y++)
					{
						list = elements[y];
						var listLength = list.Count;
						for (x = 0; x < listLength; x++) LinkElement(elements, x, y);
					}
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void LinkElement(ListGrid elements, int x, int y)
		{
			var target = elements.GetElement(primaryAxis, x, y);
			if (target == null) return;
			if (target is LayoutParentBase parentBase) parentBase.ManagedParent = this;
			int i;
			LayoutElementBase element;
			if (x > 0)
				for (i = x - 1; i >= 0; i--)
				{
					element = elements.GetElement(primaryAxis, i, y) as LayoutElementBase;
					if (element == null) continue;
					target.LeftManagedNeighbor = element;
					break;
				}
			else target.LeftManagedNeighbor = null;

			var list = elements[x];
			if (x < list.Count - 1)
				for (i = x + 1; i < list.Count; i++)
				{
					element = elements.GetElement(primaryAxis, i, y) as LayoutElementBase;
					if (element == null) continue;
					target.RightManagedNeighbor = element;
					break;
				}
			else target.RightManagedNeighbor = null;
			
			if (y > 0)
				for (i = y - 1; i >= 0; i--)
				{
					element = elements.GetElement(primaryAxis, x, i) as LayoutElementBase;
					if (element == null) continue;
					target.TopManagedNeighbor = element;
					break;
				}
			else target.TopManagedNeighbor = null;
			
			if (y < elements.Count - 1)
				for (i = y + 1; i < elements.Count; i++)
				{
					element = elements.GetElement(primaryAxis, x, i) as LayoutElementBase;
					if (element == null) continue;
					target.BottomManagedNeighbor = element;
					break;
				}
			else target.BottomManagedNeighbor = null;
		}
#endif
	}
}