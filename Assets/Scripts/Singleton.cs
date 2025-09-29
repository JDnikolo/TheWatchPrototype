﻿using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }
	
	protected virtual void Awake()
	{
		if (!Instance) Instance = this as T;
		else Destroy(gameObject);
	}

	protected virtual void OnDestroy()
	{
		if (Instance == this) Instance = null;
	}
}