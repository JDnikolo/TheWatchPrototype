using System.Collections.Generic;
using Callbacks.Dragging;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	[AddComponentMenu("UI/Drag Receptor")]
	public sealed class DragReceptor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private readonly HashSet<IBeginDragCallback> m_beginDragCallbacks = new();
		private readonly HashSet<IEndDragCallback> m_endDragCallbacks = new();
		private readonly HashSet<IDragCallback> m_dragCallbacks = new();

		public void AddReceiver(object obj)
		{
			if (obj is IBeginDragCallback beginDragCallback) m_beginDragCallbacks.Add(beginDragCallback);
			if (obj is IEndDragCallback endDragCallback) m_endDragCallbacks.Add(endDragCallback);
			if (obj is IDragCallback dragCallback) m_dragCallbacks.Add(dragCallback);
		}

		public void RemoveReceiver(object obj)
		{
			if (obj is IBeginDragCallback beginDragCallback) m_beginDragCallbacks.Remove(beginDragCallback);
			if (obj is IEndDragCallback endDragCallback) m_endDragCallbacks.Remove(endDragCallback);
			if (obj is IDragCallback dragCallback) m_dragCallbacks.Remove(dragCallback);
		}
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_beginDragCallbacks) callback.OnBeginDrag(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_dragCallbacks) callback.OnDrag(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (!enabled) return;
			foreach (var callback in m_endDragCallbacks) callback.OnEndDrag(eventData);
		}
#if UNITY_EDITOR
		public HashSet<IBeginDragCallback> BeginDragCallbacks => m_beginDragCallbacks;
		
		public HashSet<IEndDragCallback> EndDragCallbacks => m_endDragCallbacks;
		
		public HashSet<IDragCallback> DragCallbacks => m_dragCallbacks;
#endif
	}
}