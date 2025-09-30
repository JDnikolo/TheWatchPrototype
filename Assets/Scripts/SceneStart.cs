using Managers;
using UnityEngine;

public sealed class SceneStart : MonoBehaviour
{
	[SerializeField] private MonoBehaviour[] startOnScene;
	
#if UNITY_EDITOR
	public void SetArray(MonoBehaviour[] array) => startOnScene = array;
#endif
	
	private void Start()
	{
		for (var i = 0; i < startOnScene.Length; i++)
		{
			if (startOnScene[i] is IStartable startable) GameManager.Instance.AddStartable(startable);
			startOnScene[i] = null;
		}
	}
}