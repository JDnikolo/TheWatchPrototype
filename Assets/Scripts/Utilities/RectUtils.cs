using UnityEngine;

namespace Utilities
{
	public static partial class Utils
	{
		public static Vector2 GetCenter(this RectTransform rectTransform) =>
			rectTransform.position.ToScreenVector() +
			(rectTransform.pivot - new Vector2(0.5f, 0.5f)) * rectTransform.rect.size;
	}
}