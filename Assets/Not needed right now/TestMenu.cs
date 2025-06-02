using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class TestFade: MonoBehaviour
{
    public void Play()
    {
        
        LevelManager.Instance.LoadScene("Game", "CrossFade");
    }
 
    public void Quit()
    {
        Application.Quit();
    }
}