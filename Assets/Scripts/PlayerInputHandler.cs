using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FirstPersonPlayerController))]
[RequireComponent(typeof(FirstPersonCamera))]
public class PlayerInputHandler : MonoBehaviour
{
    private InputActionMap m_inputMap;

    private InputAction m_moveAction;
    private InputAction m_lookAction;

    [SerializeField]
    private string moveActionName = "Move";
    [SerializeField]
    private string lookActionName = "Look";

    private FirstPersonPlayerController m_playerController;
    private FirstPersonCamera m_camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerController = GetComponent<FirstPersonPlayerController>();
        m_camera = GetComponent<FirstPersonCamera>();
        m_inputMap = InputManager.Instance.PlayerInputMap;
        m_moveAction = m_inputMap.FindAction(moveActionName);
        m_lookAction = m_inputMap.FindAction(lookActionName);

        m_moveAction.canceled += OnMovementStopped;
        m_lookAction.canceled += OnLookingStopped;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_moveAction.triggered)
        {
            m_playerController.OnMove(m_moveAction.ReadValue<Vector2>());
        }
        if (m_lookAction.triggered)
        {
            m_camera.OnLook(m_lookAction.ReadValue<Vector2>());
        }

    }

    public void OnMovementStopped(InputAction.CallbackContext context)
    {
        m_playerController.OnMove(context.ReadValue<Vector2>());
    }

    public void OnLookingStopped(InputAction.CallbackContext context)
    {
        m_camera.OnLook(context.ReadValue<Vector2>());
    }

}
