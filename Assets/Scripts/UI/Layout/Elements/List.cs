using System;
using System.Collections.Generic;
using Exceptions;
using Runtime.Automation;
using UnityEditor;
using UnityEngine;
using Utilities;
using Node = System.Collections.Generic.LinkedListNode<UI.Layout.ILayoutElement>;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/List")]
	public sealed class List : Element, ILayoutControllingParent
	{
		public enum Mode : byte
		{
			Invisible,
			Selectable,
			Circular,
		}
		
		[SerializeField] private Axis direction;
		[SerializeField] private Mode mode;
		[SerializeField] private bool inverted;
		[SerializeField] private bool autoReset;

		private readonly LinkedList<ILayoutElement> m_elements = new();
		private Node m_selectedNode;

		public ILayoutElement FirstChild => mode == Mode.Selectable ? null : m_selectedNode?.Value;

		public void OnMissedInput(Vector2 axis, Direction input) => OnInput(axis, input);

		public ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input)
		{
			if (oldElement == null) return null;
			ILayoutElement element = this;
			if (oldElement.Parent != element) return null;
			var lastNode = FindForward(oldElement);
			if (lastNode == null) return null;
			m_selectedNode = lastNode;
			switch (mode)
			{
				case Mode.Invisible:
					return FirstChild;
				case Mode.Selectable:
					return this;
				case Mode.Circular:
					switch (input)
					{
						case UIConstants.Direction_None:
						case Direction.Left:
						case Direction.Right:
							return FirstChild;
						case Direction.Up:
							m_selectedNode = m_elements.Last;
							return FirstChild;
						case Direction.Down:
							m_selectedNode = m_elements.First;
							return FirstChild;
						default:
							throw new ArgumentOutOfRangeException(nameof(input), input, null);
					}
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement)
		{
			if (!autoReset) m_selectedNode = null;
			else m_selectedNode = FindForward(oldElement);
			if (m_selectedNode == null) SetFirstNode();
		}

		public void AddFirst(ILayoutElement element)
		{
			if (element == null) return;
			var firstElement = m_elements.First;
			m_elements.AddFirst(element);
			element.Parent = this;
			if (firstElement != null) SetFirstElement(firstElement.Value, element);
			firstElement = m_elements.First;
			SetFirstElement(element, this);
			if (firstElement == m_elements.Last) SetLastElement(element, this);
			else if (m_elements.Count > 1)
			{
				SetFirstElement(firstElement.Value, element);
				SetLastElement(element, firstElement.Value);
			}

			if (m_elements.Count == 1) SetFirstNode();
		}

		public void AddLast(ILayoutElement element)
		{
			if (element == null) return;
			var lastElement = m_elements.Last;
			m_elements.AddLast(element);
			element.Parent = this;
			if (lastElement != null) SetLastElement(lastElement.Value, element);
			lastElement = m_elements.Last;
			SetLastElement(element, this);
			if (lastElement == m_elements.First) SetFirstElement(element, this);
			else if (m_elements.Count > 1)
			{
				SetLastElement(lastElement.Value, element);
				SetFirstElement(element, lastElement.Value);
			}

			if (m_elements.Count == 1) SetFirstNode();
		}

		public void Clear()
		{
			m_elements.Clear();
			m_selectedNode = null;
		}

		public void RemoveFirst() => Remove(m_elements.First);

		public void RemoveLast() => Remove(m_elements.Last);

		public void RemoveAt(ILayoutElement element)
		{
			var node = FindForward(element);
			if (node == null) return;
			Remove(node);
		}

		public void Remove(Node node)
		{
			if (node == null) return;
			ILayoutElement element;
			if (node == m_elements.First)
			{
				element = node.Value;
				SetFirstElement(element, null);
				if (node == m_elements.Last) SetLastElement(element, null);
				else
				{
					element = m_elements.First?.Value;
					if (element != null) SetFirstElement(element, this);
				}
			}
			else if (node == m_elements.Last)
			{
				element = node.Value;
				SetLastElement(element, null);
				if (node == m_elements.First) SetFirstElement(element, null);
				else
				{
					element = m_elements.Last?.Value;
					if (element != null) SetFirstElement(element, this);
				}
			}

			element = node.Value;
			ILayoutElement previous;
			ILayoutElement next;
			switch (direction)
			{
				case Axis.Horizontal:
					previous = element.LeftNeighbor;
					next = element.RightNeighbor;
					if (IsChildElement(previous)) previous.RightNeighbor = next;
					if (IsChildElement(next)) next.LeftNeighbor = previous;
					break;
				case Axis.Vertical:
					previous = element.TopNeighbor;
					next = element.BottomNeighbor;
					if (IsChildElement(previous)) previous.BottomNeighbor = next;
					if (IsChildElement(next)) next.TopNeighbor = previous;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			m_elements.Remove(node);
		}

		private void SetFirstElement(ILayoutElement element, ILayoutElement neighbor)
		{
			switch (direction)
			{
				case Axis.Horizontal:
					element.LeftNeighbor = neighbor;
					break;
				case Axis.Vertical:
					element.TopNeighbor = neighbor;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SetLastElement(ILayoutElement element, ILayoutElement neighbor)
		{
			switch (direction)
			{
				case Axis.Horizontal:
					element.RightNeighbor = neighbor;
					break;
				case Axis.Vertical:
					element.BottomNeighbor = neighbor;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private Node FindForward(ILayoutElement element, Node node = null)
		{
			if (element != null && m_elements.Count != 0)
			{
				node ??= m_elements.First;
				do
				{
					if (node.Value == element) return node;
					node = node.Next;
				} while (node != null);
			}

			return null;
		}
		/*
		private Node FindBackward(ILayoutElement element, Node node = null)
		{
			if (element != null && m_elements.Count != 0)
			{
				node ??= m_elements.Last;
				do
				{
					if (node.Value == element) return node;
					node = node.Previous;
				} while (node != null);
			}

			return null;
		}
		*/
		private bool IsChildElement(ILayoutElement element) => element != null && ReferenceEquals(element.Parent, this);

		private void SetFirstNode()
		{
			if (inverted) m_selectedNode = m_elements.Last;
			else m_selectedNode = m_elements.First;
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			this.CollectChildren<LayoutElement, ILayoutElement>(m_elements);
			SetFirstNode();
		}

#if UNITY_EDITOR
		public Mode ListMode
		{
			get => mode;
			set => this.DirtyReplaceValue(ref mode, value);
		}

		public LinkedList<ILayoutElement> Elements => m_elements;
		
		public Node SelectedNode => m_selectedNode;

		public LayoutBlockedDirections BlockedDirections
		{
			get
			{
				int count;
				if (EditorApplication.isPlaying) count = m_elements.Count;
				else count = transform.childCount;

				if (count < 2) return LayoutBlockedDirections.All;
				switch (direction)
				{
					case Axis.Horizontal:
						return LayoutBlockedDirections.Left | LayoutBlockedDirections.Right;
					case Axis.Vertical:
						return LayoutBlockedDirections.Up | LayoutBlockedDirections.Down;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private HierarchyTester<LayoutElement> m_tester;

		protected override void OnValidate()
		{
			base.OnValidate();
			SetElementParents();
		}

		private void SetElementParents()
		{
			var parent = transform;
			var length = parent.childCount;
			var firstIndex = -1;
			int childIndex;
			for (childIndex = 0; childIndex < length; childIndex++)
			{
				if (!parent.GetChild(childIndex).GetComponent<Element>()) continue;
				firstIndex = childIndex;
				break;
			}

			if (firstIndex < 0) return;
			var firstElement = parent.GetChild(firstIndex).GetComponent<Element>();
			var lastIndex = -1;
			for (childIndex = length - 1; childIndex >= firstIndex; childIndex--)
			{
				if (!parent.GetChild(childIndex).GetComponent<Element>()) continue;
				lastIndex = childIndex;
				break;
			}

			if (lastIndex < 0) throw new HeuristicException();
			var lastElement = parent.GetChild(lastIndex).GetComponent<Element>();
			if (firstIndex == lastIndex)
			{
				if (mode == Mode.Circular)
				{
					switch (direction)
					{
						case Axis.Horizontal:
							firstElement.LeftManagedNeighbor = firstElement.RightManagedNeighbor = firstElement;
							firstElement.TopManagedNeighbor = firstElement.BottomManagedNeighbor = null;
							break;
						case Axis.Vertical:
							firstElement.TopManagedNeighbor = firstElement.BottomManagedNeighbor = firstElement;
							firstElement.LeftManagedNeighbor = firstElement.RightManagedNeighbor = null;
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}
				else
					firstElement.LeftManagedNeighbor = firstElement.RightManagedNeighbor =
						firstElement.TopManagedNeighbor = firstElement.BottomManagedNeighbor = null;
				
				firstElement.SetManagedParent(this);
				return;
			}
			
			switch (direction)
			{
				case Axis.Horizontal:
					switch (mode)
					{
						case Mode.Invisible:
							firstElement.LeftManagedNeighbor = null;
							lastElement.RightManagedNeighbor = null;
							break;
						case Mode.Selectable:
							firstElement.LeftManagedNeighbor = this;
							lastElement.RightManagedNeighbor = this;
							break;
						case Mode.Circular:
							firstElement.LeftManagedNeighbor = lastElement;
							lastElement.RightManagedNeighbor = firstElement;
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
							
					if (firstElement.TopManagedNeighbor == this) firstElement.TopManagedNeighbor = null;
					if (lastElement.BottomManagedNeighbor == this) lastElement.BottomManagedNeighbor = null;
					break;
				case Axis.Vertical:
					switch (mode)
					{
						case Mode.Invisible:
							firstElement.TopManagedNeighbor = null;
							lastElement.BottomManagedNeighbor = null;
							break;
						case Mode.Selectable:
							firstElement.TopManagedNeighbor = this;
							lastElement.BottomManagedNeighbor = this;
							break;
						case Mode.Circular:
							firstElement.TopManagedNeighbor = lastElement;
							lastElement.BottomManagedNeighbor = firstElement;
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
					
					if (firstElement.LeftManagedNeighbor == this) firstElement.LeftManagedNeighbor = null;
					if (lastElement.RightManagedNeighbor == this) lastElement.RightManagedNeighbor = null;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			lastElement.SetManagedParent(this);
			childIndex = firstIndex;
			while (childIndex < lastIndex)
			{
				var element = parent.GetChild(childIndex).GetComponent<Element>();
				element.SetManagedParent(this);
				while (childIndex <= lastIndex)
				{
					childIndex += 1;
					if (!parent.GetChild(childIndex).TryGetComponent(out Element nextElement)) continue;
					switch (direction)
					{
						case Axis.Horizontal:
							element.RightManagedNeighbor = nextElement;
							nextElement.LeftManagedNeighbor = element;
							if (element.BottomManagedNeighbor == nextElement) element.BottomManagedNeighbor = null;
							if (nextElement.TopManagedNeighbor == element) nextElement.TopManagedNeighbor = null;
							break;
						case Axis.Vertical:
							element.BottomManagedNeighbor = nextElement;
							nextElement.TopManagedNeighbor = element;
							if (element.RightManagedNeighbor == nextElement) element.RightManagedNeighbor = null;
							if (nextElement.LeftManagedNeighbor == element) nextElement.LeftManagedNeighbor = null;
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
						
					break;
				}
			}
		}
#endif
	}
}