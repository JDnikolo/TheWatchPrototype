using UnityEngine;

namespace Tests
{
	public sealed class AnimationTest : BaseBehaviour
	{
		[SerializeField] private Animator animator;

		private int m_blend;
		private float m_value;

		private void Awake() => m_blend = Animator.StringToHash("Blend");

		private void Update()
		{
			m_value += Time.deltaTime / 2f;
			animator.SetFloat(m_blend, m_value);
		}
	}
}