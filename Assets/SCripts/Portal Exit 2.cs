using System.Collections;
using UnityEngine;

public class PortalExit2 : MonoBehaviour
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
           
            try
            {
                LevelManager.Instance.LoadScene("Level 3", "CrossFade");
                Debug.Log("LoadScene called successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("LoadScene failed: " + e.Message);
            }
        }
        
    }
}