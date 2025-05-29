using System.Collections;
using UnityEngine;

public class PlayerSleepTransition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sleepingPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(SleepAndTransition());
        }
    }

    private IEnumerator SleepAndTransition()
    {
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

        LevelManager.Instance.LoadScene("Level 1", "CrossFade");
    }
}