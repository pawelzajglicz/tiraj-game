using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    AudioSource audioSource;

    private static MusicPlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<MusicPlayer>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static MusicPlayer GetInstance()
    {
        return instance;
    }


    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
