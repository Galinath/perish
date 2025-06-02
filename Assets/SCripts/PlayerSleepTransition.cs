using System.Collections;
using UnityEngine;

public class PlayerSleepTransition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sleepingPlayer;
    private bool isTransitioning = false;
    private bool isInBedArea = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isTransitioning && isInBedArea)
        {
            StartCoroutine(SleepAndTransition());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bed"))
        {
            isInBedArea = true;
            Debug.Log("Player entered bed area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bed"))
        {
            isInBedArea = false;
            Debug.Log("Player exited bed area.");
        }
    }

    private IEnumerator SleepAndTransition()
    {
        isTransitioning = true;
        Debug.Log("Starting sleep transition.");

        if (player != null)
        {
            player.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Player not assigned.");
        }

        if (sleepingPlayer != null)
        {
            sleepingPlayer.SetActive(true);
        }
        else
        {
            Debug.LogWarning("SleepingPlayer not assigned.");
        }

        yield return new WaitForSeconds(1f);

        if (LevelManager.Instance != null)
        {
            Debug.Log("Calling LevelManager.LoadScene('Level 1', 'CrossFade')");
            LevelManager.Instance.LoadScene("Level 1", "CrossFade");
        }
        else
        {
            Debug.LogError("LevelManager instance is null.");
        }
    }
}