using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public AudioSource audioSourceGameOver;

    private AudioClip gameOverSound;
    private bool soundPlayed = false;

    private void Start()
    {
        gameOverSound = (AudioClip)Resources.Load("game_over_sound");
    }

    private void Update()
    {
        if (!soundPlayed)
        {
            audioSourceGameOver.clip = gameOverSound;
            audioSourceGameOver.Play();
            soundPlayed = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            soundPlayed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
