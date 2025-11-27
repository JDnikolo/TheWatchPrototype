using System;
using System.Collections.Generic;
using Runtime.Automation;
using UnityEngine;
using Utilities;
using Node = System.Collections.Generic.LinkedListNode<UI.Layout.ILayoutElement>;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/List")]
	public sealed class List : LayoutControlled, ILayoutControllingParent
	{
		[SerializeField] private Axis direction;
		[SerializeField] private bool selectable;
		[SerializeField] private bool circular;
		[SerializeField] private bool inverted;

		private readonly LinkedList<ILayoutElement> m_elements = new();
		private Node m_firstConnectingNode;
		private Node m_lastConnectingNode;
		private Node m_selectedNode;

		public ILayoutElement FirstChild => selectable ? null : m_selectedNode?.Value;

		public ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input)
		{
			if (oldElement == null) return null;
			ILayoutElement element = this;
			if (oldElement.Parent != element) return null;
			var lastNode = FindForward(oldElement);
			if (lastNode == null) return null;
			m_selectedNode = lastNode;
			if (selectable) return this;
			if (circular)
			{
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
			}

			return FirstChild;
		}

		public void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement)
		{
			m_selectedNode = FindForward(oldElement);
			if (m_selectedNode == null) SetFirstNode();
		}

		public void AddFirst(ILayoutElement element)
		{
			if (element == null) return;
			var firstElement = m_elements.First;
			m_elements.AddFirst(element);
			element.Parent = this;
			if (element is ILayoutInput input)
			{
				if (m_firstConnectingNode != null) SetFirstElement((ILayoutInput) m_firstConnectingNode.Value, input);
				m_firstConnectingNode = m_elements.First;
				SetFirstElement(input, this);
				if (m_firstConnectingNode == m_elements.Last) SetLastElement(input, this);
				else if (m_elements.Count > 1)
				{
					if (firstElement.Value is ILayoutInput firstInput) SetFirstElement(firstInput, element);
					SetLastElement(input, firstElement.Value);
				}
			}

			if (m_elements.Count == 1) SetFirstNode();
		}

		public void AddLast(ILayoutElement element)
		{
			if (element == null) return;
			var lastElement = m_elements.Last;
			m_elements.AddLast(element);
			element.Parent = this;
			if (element is ILayoutInput input)
			{
				if (m_lastConnectingNode != null) SetLastElement((ILayoutInput) m_lastConnectingNode.Value, input);
				m_lastConnectingNode = m_elements.Last;
				SetLastElement(input, this);
				if (m_lastConnectingNode == m_elements.First) SetFirstElement(input, this);
				else if (m_elements.Count > 1)
				{
					if (lastElement.Value is ILayoutInput lastInput) SetLastElement(lastInput, element);
					SetFirstElement(input, lastElement.Value);
				}
			}

			if (m_elements.Count == 1) SetFirstNode();
		}

		public void Clear()
		{
			m_elements.Clear();
			m_firstConnectingNode = null;
			m_lastConnectingNode = null;
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
			ILayoutInput input;
			if (node == m_firstConnectingNode)
			{
				input = (ILayoutInput) m_lastConnectingNode.Value;
				SetFirstElement(input, null);
				if (m_firstConnectingNode == m_elements.Last) SetLastElement(input, null);
				else
				{
					m_firstConnectingNode = FindFirstConnectingIndex(m_firstConnectingNode.Next);
					if (m_firstConnectingNode != null) 
						SetFirstElement((ILayoutInput) m_firstConnectingNode.Value, this);
				}
			}
			else if (node == m_lastConnectingNode)
			{
				input = (ILayoutInput) m_lastConnectingNode.Value;
				SetLastElement(input, null);
				if (m_lastConnectingNode == m_elements.First) SetFirstElement(input, null);
				else
				{
					m_lastConnectingNode = FindLastConnectingIndex(m_lastConnectingNode.Previous);
					if (m_lastConnectingNode != null) 
						SetFirstElement((ILayoutInput) m_lastConnectingNode.Value, this);
				}
			}

			input = node.Value as ILayoutInput;
			if (input != null)
			{
				ILayoutInput previousInput;
				ILayoutInput nextInput;
				switch (direction)
				{
					case Axis.Horizontal:
						if (IsChildInput(input.LeftNeighbor, out previousInput)) 
							previousInput.RightNeighbor = input.RightNeighbor;
						if (IsChildInput(input.RightNeighbor, out nextInput)) 
							nextInput.LeftNeighbor = input.LeftNeighbor;
						break;
					case Axis.Vertical:
						if (IsChildInput(input.TopNeighbor, out previousInput)) 
							previousInput.BottomNeighbor = input.BottomNeighbor;
						if (IsChildInput(input.BottomNeighbor, out nextInput)) 
							nextInput.TopNeighbor = input.TopNeighbor;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			m_elements.Remove(node);
		}
		
		private void SetFirstElement(ILayoutInput element, ILayoutElement neighbor)
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

		private void SetLastElement(ILayoutInput element, ILayoutElement neighbor)
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

		private Node FindFirstConnectingIndex(Node node = null)
		{
			if (m_elements.Count != 0)
			{
				node ??= m_elements.First;
				do
				{
					if (node.Value is ILayoutInput) return node;
					node = node.Next;
				} while (node != null);
			}


			return null;
		}

		private Node FindLastConnectingIndex(Node node = null)
		{
			if (m_elements.Count != 0)
			{
				node ??= m_elements.Last;
				do
				{
					if (node.Value is ILayoutInput) return node;
					node = node.Previous;
				} while (node != null);
			}

			return null;
		}
		
		private bool IsChildElement(ILayoutElement element) => element != null && ReferenceEquals(element.Parent, this);
		
		private bool IsInput(ILayoutElement element, out ILayoutInput input)
		{
			input = element as ILayoutInput;
			return input != null;
		}

		private bool IsChildInput(ILayoutElement element, out ILayoutInput input) => 
			IsInput(element, out input) && IsChildElement(element);

		private void SetFirstNode()
		{
			if (inverted) m_selectedNode = m_elements.Last;
			else m_selectedNode = m_elements.First;
		}
		
		public override void OnPrewarm()
		{
			base.OnPrewarm();
			this.CollectChildren<LayoutElement, ILayoutElement>(m_elements);
			m_firstConnectingNode = FindFirstConnectingIndex();
			m_lastConnectingNode = FindLastConnectingIndex();
			SetFirstNode();
		}

#if UNITY_EDITOR
		public LinkedList<ILayoutElement> Elements => m_elements;
		public Node FirstConnectedNode => m_firstConnectingNode;
		public Node LastConnectedNode => m_lastConnectingNode;
		public Node SelectedNode => m_selectedNode;
		
		public LayoutBlockedDirections BlockedDirections => LayoutBlockedDirections.Up | LayoutBlockedDirections.Down;

		private HierarchyTester<LayoutElement> m_tester;

		protected override void OnValidate()
		{
			base.OnValidate();
			SetElementParents();
		}

		public override void OnHierarchyChanged()
		{
			base.OnHierarchyChanged();
			SetElementParents();
		}

		private void SetElementParents()
		{
			var parent = transform;
			var length = parent.childCount;
			ILayoutInput topInput = null;
			for (var index = 0; index < length; index++)
			{
				var managedElement = parent.GetChild(index).GetComponent<LayoutManaged>();
				if (!managedElement) continue;
				managedElement.SetManagedParent(this);
				var nextIndex = index + 1;
				var overflows = nextIndex >= length;
				if (managedElement is ILayoutInput testInput)
				{
					topInput = testInput;
					if (index == 0)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								testInput.LeftManagedNeighbor = this;
								if (testInput.TopManagedNeighbor == this) testInput.TopManagedNeighbor = null;
								break;
							case Axis.Vertical:
								testInput.TopManagedNeighbor = this;
								if (testInput.LeftManagedNeighbor == this) testInput.LeftManagedNeighbor = null;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}

					if (overflows)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								testInput.RightManagedNeighbor = this;
								if (testInput.BottomManagedNeighbor == this) testInput.BottomManagedNeighbor = null;
								break;
							case Axis.Vertical:
								testInput.BottomManagedNeighbor = this;
								if (testInput.RightManagedNeighbor == this) testInput.RightManagedNeighbor = null;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}

				if (!overflows)
				{
					var nextManagedElement = parent.GetChild(index+1).GetComponent<LayoutManaged>();
					if (nextManagedElement is ILayoutInput bottomInput && topInput != null)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								topInput.RightManagedNeighbor = nextManagedElement;
								bottomInput.LeftManagedNeighbor = managedElement;
								if (topInput.BottomManagedNeighbor == nextManagedElement) topInput.BottomManagedNeighbor = null;
								if (bottomInput.TopManagedNeighbor == managedElement) bottomInput.TopManagedNeighbor = null;
								break;
							case Axis.Vertical:
								topInput.BottomManagedNeighbor = nextManagedElement;
								bottomInput.TopManagedNeighbor = managedElement;
								if (topInput.RightManagedNeighbor == nextManagedElement) topInput.RightManagedNeighbor = null;
								if (bottomInput.LeftManagedNeighbor == managedElement) bottomInput.LeftManagedNeighbor = null;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}
			}
		}
#endif
	}
}