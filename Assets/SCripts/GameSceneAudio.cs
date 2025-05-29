using UnityEngine;

public class GameSceneAudio : MonoBehaviour
{
    [SerializeField] private AudioSource doorAudioSource;

    void Start()
    {
        if (doorAudioSource != null && doorAudioSource.clip != null)
        {
            doorAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Door AudioSource or clip not assigned.");
        }
    }
}