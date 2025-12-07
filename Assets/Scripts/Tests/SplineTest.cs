using UnityEngine;
using UnityEngine.Splines;

namespace Tests
{
	public sealed class SplineTest : BaseBehaviour
	{
		[SerializeField] private SplineContainer splineContainer;
		[SerializeField] private Transform target;
		[SerializeField] private float speed;
	
		private Spline m_spline;
		private float m_length;
	
		private float m_currentPosition;

		private void Start()
		{
			m_spline = splineContainer.Spline;
			m_length = m_spline.GetLength();
		}

		private void Update()
		{
			m_currentPosition += Time.deltaTime * speed;
			if (m_currentPosition >= m_length) m_currentPosition -= m_length;
			m_spline.Evaluate(m_currentPosition / m_length, out var position, out _, out _);
			target.position = splineContainer.transform.TransformPoint(position);
		}
	}
}
