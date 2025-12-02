using UnityEngine;

namespace Utilities
{
	public static partial class Utils
	{
		public static float GetLeft(this RectTransform rectTransform) =>
			rectTransform.position.ToScreenVector().x + (-rectTransform.pivot.x) * rectTransform.rect.width;

		public static float GetRight(this RectTransform rectTransform) =>
			rectTransform.position.ToScreenVector().x + (-rectTransform.pivot.x + 1f) * rectTransform.rect.width;
		
		public static float GetBottom(this RectTransform rectTransform) =>
			rectTransform.position.ToScreenVector().y + (rectTransform.pivot.y) * rectTransform.rect.height;
		
		public static float GetTop(this RectTransform rectTransform) =>
			rectTransform.position.ToScreenVector().y + (rectTransform.pivot.y - 1f) * rectTransform.rect.height;
		
		public static Vector2 GetCenter(this RectTransform rectTransform) =>
			rectTransform.position.ToScreenVector() +
			(rectTransform.pivot - new Vector2(0.5f, 0.5f)) * rectTransform.rect.size;
	}
}