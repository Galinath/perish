using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTraverse : MonoBehaviour
{
    // Load the game scene when Play is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene"); 
    }

   
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu"); 
    }
}

