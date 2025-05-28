using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTraverse : MonoBehaviour
{
   
    [SerializeField] private Text titleText;
    
    [SerializeField] private AudioSource audioSource;
  
    [SerializeField] private GameObject backgroundObject;

    public void Play()
    {
      
        StartCoroutine(PlayWithTitleChangeAndAudioAndSprite());
    }

    private IEnumerator PlayWithTitleChangeAndAudioAndSprite()
    {
        // Update the title text
        if (titleText != null)
        {
            titleText.text = "<color=red>Perish</color> of Man";
        }
      

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        

        // Activate the Background GameObject
        if (backgroundObject != null)
        {
            backgroundObject.SetActive(true);
        }
       

        
        yield return new WaitForSeconds(5.7f);

       
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}