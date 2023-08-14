using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer musicPlayerInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(musicPlayerInstance == null)
        {
            musicPlayerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
