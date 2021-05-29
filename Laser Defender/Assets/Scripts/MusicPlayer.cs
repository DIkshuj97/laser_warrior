using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = OptionController.GetMasterVolume();
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType(GetType()).Length>1)
        {
          Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
           
        }
    }

   public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
