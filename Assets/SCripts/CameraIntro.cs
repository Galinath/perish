using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    [SerializeField] private Transform targetPosition; // Assign a GameObject in the Inspector for the target
    [SerializeField] private float moveSpeed = 2f; // Speed of camera movement
    [SerializeField] private float positionThreshold = 0.1f; // How close the camera needs to be to the target
    [SerializeField] private DialogueBox dialogueBox; // Reference to your DialogueBox script
    private Camera mainCamera;
    private bool hasReachedTarget = false;
    private Vector3 startPosition;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
        startPosition = mainCamera.transform.position; // Store initial camera position

        // Ensure dialogue doesn't start until camera movement is complete
        if (dialogueBox != null)
        {
            dialogueBox.gameObject.SetActive(false); // Hide dialogue UI initially
        }
    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            // Move camera towards the target position
            Vector3 targetPos = new Vector3(targetPosition.position.x, targetPosition.position.y, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, moveSpeed * Time.deltaTime);

            // Check if camera is close enough to the target
            if (Vector3.Distance(mainCamera.transform.position, targetPos) < positionThreshold)
            {
                hasReachedTarget = true;
                // Activate and start dialogue
                if (dialogueBox != null)
                {
                    dialogueBox.gameObject.SetActive(true); // Show dialogue UI
                    dialogueBox.StartDialogue(); // Trigger dialogue
                }
            }
        }
    }

    // Optional: Reset camera for reuse or other scenes
    public void ResetCamera()
    {
        hasReachedTarget = false;
        mainCamera.transform.position = startPosition;
    }
}