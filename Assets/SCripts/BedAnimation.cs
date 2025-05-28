 using UnityEngine;

public class BedAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    private bool isPlayerInRange;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.T))
        {
            playerAnimator.SetTrigger("SleepTrigger");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerAnimator = other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerAnimator = null;
        }
    }
}