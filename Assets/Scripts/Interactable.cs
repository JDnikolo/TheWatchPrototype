using UnityEngine;
using UnityEngine.UI;

public class InteractableWithDialogue : MonoBehaviour
{
    [Header("Floating Name Settings")] public string displayName = "";
    public Vector3 offset = new Vector3(0, 0, 0);
    public int fontSize = 10000;
    public Color textColor = Color.white;
    public float characterSize = 0.002f;
    /*------------------------------------*/

    [Header("Player Detection")]
    public float showDistance = 3.5f; // Distance at which player can interact / see name of the object

    public string playerTag = "Player";
    /*------------------------------------*/

    [Header("Dialogue UI")] public GameObject dialoguePanel; // UI panel for dialogue
    public Text dialogueText; // Text field inside the panel
    [TextArea] public string dialogueMessage = "Hello! This is a test dialogue."; // Default dialogue message

    private GameObject m_textObject; // GameObject holding the floating 3D text
    private TextMesh m_textMesh; // The actual 3D TextMesh
    private Transform m_player; // Reference to player transform

    private bool m_isDialogueOpen; // Track if dialogue is open

    // References to player scripts (used to disable controls while in dialogue)
    // This will get replaced with the input manager, we simply swap the action map from player to ui control
    private FirstPersonPlayerController m_playerMovement;
    private FirstPersonCamera m_playerCamera;

    void Start()
    {
        InitializePlayerReferences(); // Find player + get movement/camera scripts
        CreateFloatingName(); // Create floating name above object
        HideDialogue(); // Hide dialogue UI at the start
    }

    void Update()
    {
        if (m_player == null) return;

        var distance = Vector3.Distance(m_player.position, transform.position);

        HandleFloatingName(distance); // Show/Hide floating name depending on distance
        HandleDialogueInput(distance); // Handle click to open/close dialogue
    }
#region Initialization
    /// <summary>
    /// Finds the player in the scene using tag, 
    /// and stores references to movement/camera scripts for disabling later.
    /// </summary>
    private void InitializePlayerReferences()
    {
        var playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            m_player = playerObj.transform;

            // Get player control scripts
            m_playerMovement = m_player.GetComponent<FirstPersonPlayerController>();
            m_playerCamera = m_player.GetComponentInChildren<FirstPersonCamera>();
        }
    }

    /// <summary>
    /// Creates a 3D TextMesh object above this object to display its name.
    /// </summary>
    private void CreateFloatingName()
    {
        m_textObject = new GameObject("FloatingText");
        m_textObject.transform.SetParent(transform);
        m_textObject.transform.localPosition = offset; // Offset from object center
        m_textObject.transform.localRotation = Quaternion.identity;
        m_textObject.transform.localScale = Vector3.one;

        m_textMesh = m_textObject.AddComponent<TextMesh>();
        m_textMesh.text = string.IsNullOrEmpty(displayName) ? gameObject.name : displayName;
        m_textMesh.characterSize = characterSize;
        m_textMesh.fontSize = fontSize;
        m_textMesh.color = textColor;
        m_textMesh.anchor = TextAnchor.MiddleCenter;
        m_textMesh.alignment = TextAlignment.Center;

        m_textMesh.gameObject.SetActive(false); // Hidden by default
    }
    
    /// <summary>
    /// Ensures dialogue panel is hidden at the start.
    /// </summary>
    private void HideDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
#endregion

#region Floating Name
    /// <summary>
    /// Handles showing/hiding floating name depending on player's distance.
    /// </summary>
    private void HandleFloatingName(float distance)
    {
        if (distance <= showDistance)
        {
            if (!m_textMesh.gameObject.activeSelf)
                m_textMesh.gameObject.SetActive(true);

            RotateTextTowardsPlayer();
        }
        else
        {
            if (m_textMesh.gameObject.activeSelf)
                m_textMesh.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Rotates the floating name so it always faces the player.
    /// </summary>
    private void RotateTextTowardsPlayer()
    {
        m_textObject.transform.LookAt(m_player);
        m_textObject.transform.rotation = Quaternion.LookRotation(m_textObject.transform.position - m_player.position);
    }
#endregion

#region Dialogue
    /// <summary>
    /// Handles left mouse click to open/close dialogue if in range.
    /// </summary>
    private void HandleDialogueInput(float distance)
    {
        if (distance <= showDistance && Input.GetMouseButtonDown(0))
        {
            if (!m_isDialogueOpen)
                OpenDialogue();
            else
                CloseDialogue();
        }
        else if (distance > showDistance && m_isDialogueOpen)
        {
            CloseDialogue();
        }
    }
    
    /// <summary>
    /// Opens the dialogue panel, shows the text, and disables player control.
    /// </summary>
    private void OpenDialogue()
    {
        if (dialoguePanel != null && dialogueText != null)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogueMessage;
            m_isDialogueOpen = true;
            TogglePlayerControl(false);
        }
    }

    /// <summary>
    /// Closes the dialogue panel and re-enables player control.
    /// </summary>
    private void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        m_isDialogueOpen = false;
        TogglePlayerControl(true);
    }
    
    /// <summary>
    /// Enables/disables movement and camera control while in dialogue.
    /// </summary>
    private void TogglePlayerControl(bool enabled)
    {
        if (m_playerMovement != null) m_playerMovement.enabled = enabled;
        if (m_playerCamera != null) m_playerCamera.enabled = enabled;
    }
#endregion
}