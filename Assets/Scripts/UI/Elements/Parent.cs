using Attributes;
using Callbacks.Layout;
using Callbacks.Pointer;
using Callbacks.Prewarm;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
	public class Parent : ElementBase, IPointerEnterCallback, IPointerExitCallback, 
		ILayoutCallback, ILayoutExplicitCallback, IPrewarm
	{
		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(Image))]
		private Image image;
		
		[SerializeField] private ElementColor color;

		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(PointerReceptor))] 
		private PointerReceptor pointerReceptor;
		
		private bool m_selected;
		private bool m_explicit;
		
		protected Image Image => image;
		
		protected ElementColor Color => color;
		
		public bool Selected => m_selected;

		public bool Explicit => m_explicit;

		protected virtual void Select()
		{
			color.ApplySelected(image);
			m_selected = true;
		}

		protected virtual void Deselect()
		{
			color.ApplyEnabled(image);
			m_selected = false;
		}
		
		public void OnSelected()
		{
			if (m_explicit) return;
			Select();
		}

		public void OnDeselected()
		{
			if (m_explicit) return;
			Deselect();
		}

		public void OnSelectedExplicit()
		{
			if (!m_explicit) return;
			Select();
		}

		public void OnDeselectedExplicit()
		{
			if (!m_explicit) return;
			Deselect();
		}

		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			if (LayoutParent) LayoutManager.Instance.Select(LayoutParent);
			else OnSelected();
		}

		public virtual void OnPointerExit(PointerEventData eventData)
		{
			if (!LayoutParent) OnDeselected();
		}

		public virtual void OnPrewarm()
		{
			if (LayoutParent)
			{
				if (LayoutParent is ILayoutExplicitCallbackImplementer implementer && implementer.IsExplicit)
				{
					m_explicit = true;
					implementer.SetExplicitCallback(this);
				}
				else LayoutParent.SetCallback(this);
			}

			pointerReceptor?.AddReceiver(this);
		}

		protected virtual void OnEnable()
		{
			if (m_selected) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		protected virtual void OnDisable() => color.ApplyDisabled(image);

		protected virtual void OnDestroy() => pointerReceptor?.RemoveReceiver(this);
#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			if (image && color) color.Validate(image, enabled);
		}
#endif
	}
}