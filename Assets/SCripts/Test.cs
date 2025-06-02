using UnityEngine;
public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Testing LevelManager.");
            LevelManager.Instance.LoadScene("Level 1", "CrossFade");
        }
    }
}