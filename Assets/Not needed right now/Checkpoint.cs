using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite unlitTorchSprite; // Assign unlit torch sprite in Inspector
    [SerializeField] private Sprite litTorchSprite;   // Assign lit torch sprite in Inspector
    [SerializeField] private AudioClip checkpointSound; // Assign audio clip in Inspector
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isActivated = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unlitTorchSprite; // Set initial sprite to unlit
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = checkpointSound; // Set the audio clip
        audioSource.playOnAwake = false; // Prevent playing on start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            other.GetComponent<Respawn>()?.SetCheckpoint(transform.position);
            spriteRenderer.sprite = litTorchSprite; // Switch to lit sprite
            audioSource.Play(); // Play the audio clip
            isActivated = true; // Prevent sprite change and sound from replaying
        }
    }
}