using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotKeys : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
