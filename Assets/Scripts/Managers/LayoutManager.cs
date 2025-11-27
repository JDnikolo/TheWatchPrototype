using System.Collections.Generic;
using Managers.Persistent;
using Runtime;
using Runtime.FrameUpdate;
using UI;
using UI.Layout;
using UnityEngine;
using UnityEngine.InputSystem;
using ILayoutElement = UI.Layout.ILayoutElement;

namespace Managers
{
	[AddComponentMenu("Managers/Layout Manager")]
	public sealed partial class LayoutManager : Singleton<LayoutManager>, IFrameUpdatable
	{
		[SerializeField] private string navigateActionName = "Navigate";

		private List<ILayoutElement> m_parentHierarchy = new();
		private ILayoutElement m_currentElement;
		private ILayoutInput m_currentInput;

		private Stack<ILayoutElement> m_tempStack = new();
		private InputAction m_navigateAction;
		private Direction m_input;
		private Updatable m_updatable;
		private bool m_ignoreUpdate;

		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.LayoutManager;

		public State PauseState
		{
			get => new()
			{
				ParentHierarchy = m_parentHierarchy.ToArray(),
				CurrentElement = m_currentElement,
				CurrentInput = m_currentInput,
			};
			set
			{
				m_ignoreUpdate = true;
				Select(null);
				m_ignoreUpdate = false;
				m_parentHierarchy.AddRange(value.ParentHierarchy);
				m_currentElement = value.CurrentElement;
				m_updatable.SetUpdating((m_currentInput = m_currentElement as ILayoutInput) != null, this);
			}
		}

		public void OnFrameUpdate()
		{
			m_navigateAction ??= InputManager.Instance.UIMap.GetAction(navigateActionName);
			var currentInput = m_currentInput;
			if (currentInput != null)
			{
				Direction input;
				Vector2 axis;
				if (!m_navigateAction.IsPressed()) axis = Vector2.zero;
				else axis = m_navigateAction.ReadValue<Vector2>();
				
				if (axis.x < 0f) input = Direction.Left;
				else if (axis.x > 0f) input = Direction.Right;
				else if (axis.y < 0f) input = Direction.Down;
				else if (axis.y > 0f) input = Direction.Up;
				else input = UIConstants.Direction_None;

				if (m_input != input) m_input = input;
				else input = UIConstants.Direction_None;
				currentInput.OnInput(axis, input);
			}
		}

		public void Select(ILayoutElement element, Direction input = UIConstants.Direction_None)
		{
			if (element != null)
			{
				var parent = element as ILayoutParent;
				while (parent != null)
				{
					ILayoutElement tempElement;
					if (element is ILayoutControllingParent controllingParent)
					{
						tempElement = controllingParent.OnSelectNewElement(m_currentElement, input);
						if (tempElement != null)
						{
							element = tempElement;
							break;
						}
					}
					
					tempElement = parent.FirstChild;
					if (tempElement == null) break;
					element = tempElement;
					parent = element as ILayoutParent;
				}
			}
			
			if (m_currentElement == element) return;
			int index;
			if (m_currentElement != null)
			{
				m_currentElement.Deselect();
				PushParents(element);
				index = 0;
				var parents = m_parentHierarchy.Count;
				while (m_tempStack.TryPop(out var parent))
				{
					if (index >= parents) break;
					var oldParent = m_parentHierarchy[index];
					if (oldParent != parent) break;
					index += 1;
				}

				m_tempStack.Clear();
				index -= 1;
				for (var i = m_parentHierarchy.Count - 1; i > index; i--)
				{
					var parent = m_parentHierarchy[i];
					if (parent is ILayoutControllingParent controllingParent) 
						controllingParent.OnSelectingNewHierarchy(element, m_currentElement);
					parent.Deselect();
					m_parentHierarchy.RemoveAt(i);
				}
			}

			if (element is ILayoutHook hook) hook.OnHookInput(m_currentElement, input);
			m_currentElement = element;
			m_currentInput = element as ILayoutInput;
			if (!m_ignoreUpdate) m_updatable.SetUpdating(m_currentInput != null, this);
			if (element != null)
			{
				PushParents(element);
				index = 0;
				var parents = m_parentHierarchy.Count;
				while (m_tempStack.TryPop(out var parent))
				{
					if (index >= parents || m_parentHierarchy[index] != parent)
					{
						parent.Select();
						m_parentHierarchy.Add(parent);
					}

					index += 1;
				}

				m_tempStack.Clear();
				element.Select();
			}
		}

		internal void ForceSelect(ILayoutElement element)
		{
			m_parentHierarchy.Clear();
			m_currentElement = null;
			m_currentInput = null;
			Select(element);
		}
		
		private void PushParents(ILayoutElement element)
		{
			if (element == null) return;
			var parent = element.Parent;
			while (parent != null)
			{
				m_tempStack.Push(parent);
				parent = parent.Parent;
			}
		}
		
		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_parentHierarchy.Clear();
			m_parentHierarchy = null;
			m_tempStack.Clear();
			m_tempStack = null;
			m_currentElement = null;
		}
#if UNITY_EDITOR
		public List<ILayoutElement> ParentHierarchy => m_parentHierarchy;
		public ILayoutElement CurrentElement => m_currentElement;
		public ILayoutInput CurrentInput => m_currentInput;
#endif
	}
}