using System.Collections;
using UnityEngine;

public class PortalExit: MonoBehaviour
{
    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    private IEnumerator TransitionToNextScene()
    {
        isTransitioning = true;

        yield return new WaitForSeconds(0.5f);

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadScene("Level 2", "CrossFade");
        }
    }
}