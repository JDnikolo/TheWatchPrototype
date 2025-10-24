using Managers;
using UnityEngine;

public class SettingsTest : MonoBehaviour, IStartable
{
	public byte StartOrder => 0;

	public void OnStart() => SettingsManager.Instance.Save();
}