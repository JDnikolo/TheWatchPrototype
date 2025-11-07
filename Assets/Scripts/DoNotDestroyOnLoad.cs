using UnityEngine;

public sealed class DoNotDestroyOnLoad : MonoBehaviour
{
	private static DoNotDestroyOnLoad m_instance;
	
	private void Awake()
	{
		if (!m_instance)
		{
			m_instance = this;
			DontDestroyOnLoad(this);
		}
		else Destroy(gameObject);
	}
}