using Interactables;
using UI.Loading;
using UnityEngine;

namespace Tests
{
	public sealed class LoadingTest : Interactable
	{
		[SerializeField] private LoadingBar loadingBar;
		[SerializeField] private float loadingDuration;

		private float m_progress;
		
		private void Update()
		{
			var deltaTime = Time.deltaTime / loadingDuration;
			m_progress += deltaTime;
			if (m_progress >= 1f)
			{
				m_progress = 1f;
				enabled = false;
			}
			
			loadingBar.SetProgress(m_progress);
		}

		public override void Interact() => enabled = true;

		private void OnValidate() => enabled = false;
	}
}