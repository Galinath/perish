using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Respawn respawn = other.GetComponent<Respawn>();
            if (respawn != null)
            {
                respawn.Die();
            }
        }
    }
}
