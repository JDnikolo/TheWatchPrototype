using System;
using System.Collections.Generic;
using Runtime;
using UI.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Scene Manager")]
	public sealed class SceneManager : Singleton<SceneManager>
	{
		[SerializeField] private LoadingBar loadingBar;

		private Stack<Scene> m_scenesToUnload = new();
		private AsyncOperation m_asyncOperation;
		private string m_sceneName;

		protected override bool Override => false;

		public static Scene GetActiveScene() => UnitySceneManager.GetActiveScene();
		
		public void LoadNewScene(string name)
		{
			m_sceneName = name;
			CameraManager.Instance.SetPersistent();
			loadingBar.gameObject.SetActive(true);
			var loadedScenes = UnitySceneManager.loadedSceneCount;
			for (var i = 1; i < loadedScenes; i++) m_scenesToUnload.Push(UnitySceneManager.GetSceneAt(i));
			var gameManager = GameManager.Instance;
			gameManager.InvokeOnNextFrameUpdate(gameManager.BeginLoad);
		}

		internal void ProcessScenes()
		{
			if (m_asyncOperation != null)
			{
				var progress = m_asyncOperation.progress;
				if (progress >= 1f)
				{
					progress = 1f;
					m_asyncOperation = null;
				}

				loadingBar.SetProgress(progress);
				return;
			}

			Retry:
			if (m_scenesToUnload.TryPop(out var scene))
			{
				m_asyncOperation = UnitySceneManager.UnloadSceneAsync(scene);
				if (m_asyncOperation == null) goto Retry;
				m_asyncOperation.allowSceneActivation = true;
				return;
			}

			if (m_sceneName != null)
			{
				m_asyncOperation = UnitySceneManager.LoadSceneAsync(m_sceneName);
				if (m_asyncOperation == null)
					throw new InvalidOperationException($"Could not load scene {m_sceneName}");
				m_asyncOperation.allowSceneActivation = true;
				m_sceneName = null;
			}

			loadingBar.gameObject.SetActive(false);
			GameManager.Instance.EndLoad();
		}
	}
}