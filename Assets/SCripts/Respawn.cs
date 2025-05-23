using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector2 respawnPoint;

    void Start()
    {
        respawnPoint = transform.position;
    }

    public void SetCheckpoint(Vector2 point)
    {
        respawnPoint = point;
    }

    public void Die()
    {
        transform.position = respawnPoint;
    }
}
