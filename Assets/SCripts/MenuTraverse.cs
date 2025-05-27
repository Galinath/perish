using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For legacy Text component

public class MainTraverse : MonoBehaviour
{
    
    [SerializeField] private Text titleText;
    
    [SerializeField] private AudioSource audioSource;

    public void Play()
    {

        StartCoroutine(PlayWithTitleChangeAndAudio());
        LevelManager.Instance.LoadScene("Game", "CrossFade");
        
    }

    private IEnumerator PlayWithTitleChangeAndAudio()
    {
        
        if (titleText != null)
        {
            titleText.text = "<color=red>Perish</color> of Man";
        }
        

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
       
       
        yield return new WaitForSeconds(5.7f);

        
       
    }

    public void Quit()
    {
        Application.Quit();
    }
}