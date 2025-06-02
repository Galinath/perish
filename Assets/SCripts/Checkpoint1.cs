using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Respawn>()?.SetCheckpoint(transform.position);
        }
    }
}
