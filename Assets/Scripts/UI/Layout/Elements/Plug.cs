using System;
using Managers;
using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Plug")]
	public sealed class Plug : LayoutElement, ILayoutParent, ILayoutHook, ILayoutInput
#if UNITY_EDITOR
		, IHierarchyChanged
#endif
	{
		[SerializeField] private LayoutElement child;

		private ILayoutElement m_lastElement;
		private Direction m_lastInput;
		
		public override ILayoutElement Parent
		{
			get => null;
			set => throw new InvalidOperationException();
		}
		
		public ILayoutElement FirstChild => child;
		
		public ILayoutElement LeftNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public ILayoutElement RightNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public ILayoutElement TopNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public ILayoutElement BottomNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
		
		public void OnHookInput(ILayoutElement oldElement, Direction input)
		{
			m_lastElement = oldElement;
			m_lastInput = input.Invert();
		}

		public void OnInput(Vector2 axis, Direction input)
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
		
		public LayoutElement LeftManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public LayoutElement RightManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public LayoutElement TopManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}

		public LayoutElement BottomManagedNeighbor
		{
			get => null;
			set => throw new InvalidOperationException();
		}
#endif
	}
}