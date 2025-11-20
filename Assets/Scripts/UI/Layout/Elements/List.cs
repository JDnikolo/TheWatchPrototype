using System;
using Runtime.Automation;
using UnityEngine;
using Utilities;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/List")]
	public sealed class List : LayoutControlled, ILayoutControllingParent
	{
		[SerializeField] private Axis direction;
		[SerializeField] private bool selectable;
		[SerializeField] private bool circular;
		[SerializeField] private LayoutElement[] elements;

		public ILayoutElement FirstChild
		{
			get
			{
				if (!selectable && elements.Length != 0 && elements[m_lastIndex] is ILayoutInput)
					return elements[m_lastIndex];
				return null;
			}
		}

		private int m_lastIndex;

		public ILayoutElement OnSelectNewElement(ILayoutElement oldElement, Direction input)
		{
			if (oldElement == null) return null;
			ILayoutElement element = this;
			if (oldElement.Parent != element) return null;
			var index = FindElement(oldElement);
			if (index < 0) return null;
			m_lastIndex = index;
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
						m_lastIndex = elements.Length - 1;
						return FirstChild;
					case Direction.Down:
						m_lastIndex = 0;
						return FirstChild;
					default:
						throw new ArgumentOutOfRangeException(nameof(input), input, null);
				}
			}

			return FirstChild;
		}

		public void OnSelectingNewHierarchy(ILayoutElement newElement, ILayoutElement oldElement)
		{
			var index = FindElement(oldElement);
			if (index < 0) index = 0;
			m_lastIndex = index;
		}

		private int FindElement(ILayoutElement oldElement)
		{
			var index = -1;
			for (var i = 0; i < elements.Length; i++)
			{
				ILayoutElement element = elements[i];
				if (element == oldElement)
				{
					index = i;
					break;
				}
			}

			return index;
		}

#if UNITY_EDITOR
		public LayoutBlockedDirections BlockedDirections => LayoutBlockedDirections.Up | LayoutBlockedDirections.Down;

		private HierarchyTester<LayoutElement> m_tester;

		protected override void OnValidate()
		{
			base.OnValidate();
			if (!ShouldChangeParents()) return;
			SetElementParents();
		}

		private bool ShouldChangeParents()
		{
			var length = elements.Length;
			ILayoutElement parent = this;
			ILayoutInput topInput = null;
			for (var index = 0; index < length; index++)
			{
				var element = elements[index];
				if (element.Parent != parent) return true;
				var nextIndex = index + 1;
				var overflows = nextIndex >= length;
				if (element is ILayoutInput testInput)
				{
					topInput = testInput;
					if (index == 0)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								if (testInput.LeftNeighbor != this || testInput.TopNeighbor == this) return true;
								break;
							case Axis.Vertical:
								if (testInput.TopNeighbor != this || testInput.LeftNeighbor == this) return true;
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
								if (testInput.RightNeighbor != this || testInput.BottomNeighbor == this) return true;
								break;
							case Axis.Vertical:
								if (testInput.BottomNeighbor != this || testInput.RightNeighbor == this) return true;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}

				if (!overflows)
				{
					var nextElement = elements[index + 1];
					if (nextElement is ILayoutInput bottomInput && topInput != null)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								if (topInput.RightNeighbor != nextElement || bottomInput.LeftNeighbor != element ||
									topInput.BottomNeighbor == nextElement || bottomInput.TopNeighbor == element)
									return true;
								break;
							case Axis.Vertical:
								if (topInput.BottomNeighbor != nextElement || bottomInput.TopNeighbor != element ||
									topInput.RightNeighbor == nextElement || bottomInput.LeftNeighbor == element)
									return true;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}
			}

			return false;
		}

		public override void OnHierarchyChanged()
		{
			base.OnHierarchyChanged();
			if (!m_tester.PerformTest(this, ref elements)) return;
			SetElementParents();
		}

		private void SetElementParents()
		{
			var length = elements.Length;
			ILayoutInput topInput = null;
			for (var index = 0; index < length; index++)
			{
				var element = elements[index];
				element.SetManagedParent(this);
				var nextIndex = index + 1;
				var overflows = nextIndex >= length;
				if (element is ILayoutInput testInput)
				{
					topInput = testInput;
					if (index == 0)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								testInput.LeftNeighbor = this;
								if (testInput.TopNeighbor == this) testInput.TopNeighbor = null;
								break;
							case Axis.Vertical:
								testInput.TopNeighbor = this;
								if (testInput.LeftNeighbor == this) testInput.LeftNeighbor = null;
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
								testInput.RightNeighbor = this;
								if (testInput.BottomNeighbor == this) testInput.BottomNeighbor = null;
								break;
							case Axis.Vertical:
								testInput.BottomNeighbor = this;
								if (testInput.RightNeighbor == this) testInput.RightNeighbor = null;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}

				if (!overflows)
				{
					var nextElement = elements[index + 1];
					if (nextElement is ILayoutInput bottomInput && topInput != null)
					{
						switch (direction)
						{
							case Axis.Horizontal:
								topInput.RightNeighbor = nextElement;
								bottomInput.LeftNeighbor = element;
								if (topInput.BottomNeighbor == nextElement) topInput.BottomNeighbor = null;
								if (bottomInput.TopNeighbor == element) bottomInput.TopNeighbor = null;
								break;
							case Axis.Vertical:
								topInput.BottomNeighbor = nextElement;
								bottomInput.TopNeighbor = element;
								if (topInput.RightNeighbor == nextElement) topInput.RightNeighbor = null;
								if (bottomInput.LeftNeighbor == element) bottomInput.LeftNeighbor = null;
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