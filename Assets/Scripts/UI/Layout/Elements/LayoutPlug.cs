using System;
using Attributes;
using Managers;
using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Plug")]
	public sealed class LayoutPlug : LayoutElementBase, ILayoutParent, ILayoutHook
#if UNITY_EDITOR
		, IHierarchyChanged
#endif
	{
		[SerializeField, AutoAssigned(AssignModeFlags.Child, typeof(LayoutElementBase))]
		private LayoutElementBase child;

		private ILayoutElement m_lastElement;
		private Direction m_lastInput;
		
		public override ILayoutElement Parent
		{
			get => null;
			set => throw new InvalidOperationException();
		}
		
		public ILayoutElement FirstChild => child;

		public override ILayoutElement LeftNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement RightNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement TopNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override ILayoutElement BottomNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
		
		public void OnHookInput(ILayoutElement oldElement, Direction input)
		{
			m_lastElement = oldElement;
			m_lastInput = input.Invert();
		}

		public override void OnInput(Vector2 axis, Direction input)
		{
			if (m_lastInput == UIConstants.Direction_None)
			{
				if (input != UIConstants.Direction_None) LayoutManager.Instance.Select(m_lastElement, input);
				return;
			}
			
			if (input == m_lastInput) LayoutManager.Instance.Select(m_lastElement, input);
		}

		public override void Select() => gameObject.SetActive(true);

		public override void Deselect() => gameObject.SetActive(false);
#if UNITY_EDITOR
		public void OnHierarchyChanged() => child.SetManagedParent(this);
		
		public override LayoutElementBase LeftManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElementBase RightManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElementBase TopManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public override LayoutElementBase BottomManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
#endif
	}
}