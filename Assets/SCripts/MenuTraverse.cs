using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainTraverse : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;

    public void Play()
    {
        StartCoroutine(PlayWithEffects());
    }

    private IEnumerator PlayWithEffects()
    {
        if (titleText != null)
        {
            titleText.text = "<color=red>Perish</color> of Man";
        }
      

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        

        if (background1 != null)
        {
            background1.SetActive(true);
        }
      

        yield return new WaitForSeconds(1f);

        if (background1 != null)
        {
            background1.SetActive(false);
        }
        if (background2 != null)
        {
            background2.SetActive(true);
        }
      
        yield return new WaitForSeconds(2.8f);

        LevelManager.Instance.LoadScene("Game", "CrossFade");
    }

    public void Quit()
    {
        Application.Quit();
    }
}