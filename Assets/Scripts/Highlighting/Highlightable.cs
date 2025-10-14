﻿using UnityEngine;

namespace Highlighting
{
	public abstract class Highlightable : MonoBehaviour, IHighlightable
	{
		private bool m_highlighted;
		
		public void Highlight(bool enabled)
		{
			if (m_highlighted == enabled) return;
			HighlightInternal(m_highlighted = enabled);
		}

		protected abstract void HighlightInternal(bool enabled);
	}
}