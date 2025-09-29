using UnityEngine;

public sealed class DoNotDestroyOnLoad : MonoBehaviour
{
	private void Awake() => DontDestroyOnLoad(this);
}