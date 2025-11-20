using UI.Elements;
using UnityEngine;

namespace UI.Layout.Elements
{
	[AddComponentMenu("UI/Layout/Layout Element")]
	public sealed class Element : LayoutControlled
	{
		private ILayoutCallback m_callback;
		
		public void SetCallback(ILayoutCallback callback) => m_callback = callback;
		
		public override void Select()
		{
			base.Select();
			m_callback?.OnSelected();
		}

		public override void Deselect()
		{
			base.Deselect();
			m_callback?.OnDeselected();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			SetCallback(null);
		}

#if UNITY_EDITOR
		public override void OnHierarchyChanged()
		{
			base.OnHierarchyChanged();
			if (TryGetComponent(out ElementBase elementBase)) elementBase.SetLayoutParent(this);
		}
#endif
	}
}