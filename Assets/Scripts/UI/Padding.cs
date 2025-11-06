using System;
using UnityEngine;

namespace UI
{
	[Serializable]
	public struct Padding
	{
		[SerializeField] private float top;
		[SerializeField] private float bottom;
		[SerializeField] private float left;
		[SerializeField] private float right;

		public float Top => top;
		public float Bottom => bottom;
		public float Left => left;
		public float Right => right;
	}
}