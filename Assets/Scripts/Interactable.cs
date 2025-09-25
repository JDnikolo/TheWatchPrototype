using UnityEngine;
using UnityEngine.UI;

public class InteractableWithDialogue : MonoBehaviour
{
    [Header("Floating Name Settings")]
    public string displayName = "";       
    public Vector3 offset = new Vector3(0, 0, 0);
    public int fontSize = 10000;
    public Color textColor = Color.white;
    public float characterSize = 0.002f;
    /*------------------------------------*/

    [Header("Player Detection")]
    public float showDistance = 3.5f;   // Distance at which player can interact / see name of the object
    public string playerTag = "Player";
    /*------------------------------------*/

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;   // UI panel for dialogue
    public Text dialogueText;          // Text field inside the panel
    [TextArea] public string dialogueMessage = "Hello! This is a test dialogue."; // Default dialogue message

    private GameObject textObject; // GameObject holding the floating 3D text
    private TextMesh textMesh;     // The actual 3D TextMesh
    private Transform player;      // Reference to player transform

    private bool isDialogueOpen = false; // Track if dialogue is open

    // References to player scripts (used to disable controls while in dialogue)
    private FirstPersonPlayerController playerMovement;
    private FirstPersonCamera playerCamera;



    void Start()
    {
        InitializePlayerReferences(); // Find player + get movement/camera scripts
        CreateFloatingName();         // Create floating name above object
        HideDialogue();               // Hide dialogue UI at the start
    }



    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        HandleFloatingName(distance);   // Show/Hide floating name depending on distance
        HandleDialogueInput(distance);  // Handle click to open/close dialogue
    }




    #region Initialization



    /// <summary>
    /// Finds the player in the scene using tag, 
    /// and stores references to movement/camera scripts for disabling later.
    /// </summary>
    private void InitializePlayerReferences()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;

            // Get player control scripts
            playerMovement = player.GetComponent<FirstPersonPlayerController>();
            playerCamera = player.GetComponentInChildren<FirstPersonCamera>();
        }
    }



    /// <summary>
    /// Creates a 3D TextMesh object above this object to display its name.
    /// </summary>
    private void CreateFloatingName()
    {
        textObject = new GameObject("FloatingText");
        textObject.transform.SetParent(transform);
        textObject.transform.localPosition = offset; // Offset from object center
        textObject.transform.localRotation = Quaternion.identity;
        textObject.transform.localScale = Vector3.one;

        textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = string.IsNullOrEmpty(displayName) ? gameObject.name : displayName;
        textMesh.characterSize = characterSize;
        textMesh.fontSize = fontSize;
        textMesh.color = textColor;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;

        textMesh.gameObject.SetActive(false); // Hidden by default
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
            if (!textMesh.gameObject.activeSelf)
                textMesh.gameObject.SetActive(true);

            RotateTextTowardsPlayer();
        }
        else
        {
            if (textMesh.gameObject.activeSelf)
                textMesh.gameObject.SetActive(false);
        }
    }




    /// <summary>
    /// Rotates the floating name so it always faces the player.
    /// </summary>
    private void RotateTextTowardsPlayer()
    {
        textObject.transform.LookAt(player);
        textObject.transform.rotation = Quaternion.LookRotation(textObject.transform.position - player.position);
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
            if (!isDialogueOpen)
                OpenDialogue();
            else
                CloseDialogue();
        }
        else if (distance > showDistance && isDialogueOpen)
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
            isDialogueOpen = true;
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

        isDialogueOpen = false;
        TogglePlayerControl(true);
    }



    /// <summary>
    /// Enables/disables movement and camera control while in dialogue.
    /// </summary>
    private void TogglePlayerControl(bool enabled)
    {
        if (playerMovement != null) playerMovement.enabled = enabled;
        if (playerCamera != null) playerCamera.enabled = enabled;
    }
    #endregion
}
