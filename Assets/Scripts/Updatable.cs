using Managers;

public struct Updatable
{
	private bool m_updating;
	
	public void SetUpdating(bool updating, IFrameUpdatable frameUpdatable = null, IFixedUpdatable fixedUpdatable = null)
	{
		if (m_updating == updating) return;
		var gameManager = GameManager.Instance;
		if (gameManager == null) return;
		m_updating = updating;
		if (m_updating)
		{
			if (frameUpdatable != null) gameManager.AddFrameUpdate(frameUpdatable);
			if (fixedUpdatable != null) gameManager.AddFixedUpdate(fixedUpdatable);
		}
		else
		{
			if (frameUpdatable != null) gameManager.RemoveFrameUpdate(frameUpdatable);
			if (fixedUpdatable != null) gameManager.RemoveFixedUpdate(fixedUpdatable);
		}
	}
}