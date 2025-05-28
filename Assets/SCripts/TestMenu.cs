using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class TestMenu: MonoBehaviour
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