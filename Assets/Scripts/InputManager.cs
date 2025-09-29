using NUnit.Framework.Internal.Commands;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager m_instance;
    public static InputManager Instance { get { return m_instance; } }

    [SerializeField]
    private InputActionAsset actionAsset;

    [SerializeField]
    private string uiMapName = "UI";

    [SerializeField]
    private string playerMapName = "Player";

    private InputActionMap m_uiMap;
    private InputActionMap m_playerMap;

    public InputActionMap UIInputMap { get => m_uiMap; }
    public InputActionMap PlayerInputMap { get => m_playerMap; }

    public bool IsPlayerInputEnabled { get => m_playerMap.enabled; }
    public bool IsUIInputEnabled { get => m_uiMap.enabled; }

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Cursor.lockState = CursorLockMode.Locked;

        m_uiMap = actionAsset.FindActionMap(uiMapName);
        m_playerMap = actionAsset.FindActionMap(playerMapName);
    }

    public void SetCursorLock(bool locked = true)
    {
        if (locked) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = !locked;
    }

    #region Input Map Handling
    /// <summary>
    /// Enable or disable the Player input map.
    /// </summary>
    /// <param name="enabled">True to enable, false to disable.</param>
    private void EnablePlayerInput(bool enabled = true)
    {
        if (enabled) m_playerMap.Enable();
        else m_playerMap.Disable();
    }
    /// <summary>
    /// Enable or disable the UI input map.
    /// </summary>
    /// <param name="enabled">True to enable, false to disable.</param>
    private void EnableUIInput(bool enabled = true)
    {
        if (enabled) m_uiMap.Enable();
        else m_uiMap.Disable();
    }

    /// <summary>
    /// Enable the UI input map, disabling the Player input map. 
    /// </summary>
    public void ChangeToUIInputMap()
    {
        EnablePlayerInput(false);
        SetCursorLock(false);
        EnableUIInput(true);
    }

    /// <summary>
    /// Enable the Player input map, disabling the UI input map. 
    /// </summary>
    public void ChangeToPlayerInputMap()
    {
        EnablePlayerInput(true);
        SetCursorLock(true);
        EnableUIInput(false);
    }
    /// <summary>
    /// Switch from the UI input map to the Player input map and vice versa.
    /// </summary>
    public void SwitchMaps()
    {
        var status = IsPlayerInputEnabled;

        EnablePlayerInput(!status);

        SetCursorLock(!status);

        EnableUIInput(status);
    }
    #endregion
}
