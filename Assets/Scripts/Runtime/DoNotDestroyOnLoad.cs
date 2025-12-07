namespace Runtime
{
	public sealed class DoNotDestroyOnLoad : BaseBehaviour
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
}