using System.Collections;
using UnityEngine;

public class PortalExit : MonoBehaviour
{
    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            Debug.Log("Player entered portal.");
            StartCoroutine(TransitionToNextScene());
        }
    }

    private IEnumerator TransitionToNextScene()
    {
        isTransitioning = true;

        yield return new WaitForSeconds(0.1f);

        if (LevelManager.Instance != null)
        {
            Debug.Log("Attempting to load 'Level 2' with CrossFade.");
            try
            {
                LevelManager.Instance.LoadScene("Level 2", "CrossFade");
                Debug.Log("LoadScene called successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("LoadScene failed: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("LevelManager instance is null.");
        }
    }
}