using System.Collections.Generic;
using Callbacks.Pointer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	[AddComponentMenu("UI/Pointer Receptor")]
	public sealed class PointerReceptor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
		IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
	{
		private HashSet<IPointerEnterCallback> m_enterCallback = new();
		private HashSet<IPointerExitCallback> m_exitCallback = new();
		private HashSet<IPointerDownCallback> m_downCallback = new();
		private HashSet<IPointerUpCallback> m_upCallback = new();
		private HashSet<IPointerClickCallback> m_clickCallback = new();

		public void AddReceiver(object obj)
		{
			if (obj is IPointerEnterCallback enterCallback) m_enterCallback.Add(enterCallback);
			if (obj is IPointerExitCallback exitCallback) m_exitCallback.Add(exitCallback);
			if (obj is IPointerDownCallback downCallback) m_downCallback.Add(downCallback);
			if (obj is IPointerUpCallback upCallback) m_upCallback.Add(upCallback);
			if (obj is IPointerClickCallback clickCallback) m_clickCallback.Add(clickCallback);
		}

		public void RemoveReceiver(object obj)
		{
			if (obj is IPointerEnterCallback enterCallback) m_enterCallback.Remove(enterCallback);
			if (obj is IPointerExitCallback exitCallback) m_exitCallback.Remove(exitCallback);
			if (obj is IPointerDownCallback downCallback) m_downCallback.Remove(downCallback);
			if (obj is IPointerUpCallback upCallback) m_upCallback.Remove(upCallback);
			if (obj is IPointerClickCallback clickCallback) m_clickCallback.Remove(clickCallback);
		}
		
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_enterCallback) callback.OnPointerEnter(eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_exitCallback) callback.OnPointerExit(eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_downCallback) callback.OnPointerDown(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_upCallback) callback.OnPointerUp(eventData);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_clickCallback) callback.OnPointerClick(eventData);
		}
#if UNITY_EDITOR
		public HashSet<IPointerEnterCallback> EnterCallbacks => m_enterCallback;
		
		public HashSet<IPointerExitCallback> ExitCallbacks => m_exitCallback;
		
		public HashSet<IPointerDownCallback> DownCallbacks => m_downCallback;
		
		public HashSet<IPointerUpCallback> UpCallbacks => m_upCallback;
		
		public HashSet<IPointerClickCallback> ClickCallbacks => m_clickCallback;
#endif
	}
}