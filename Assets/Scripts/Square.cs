using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioSource audioSourceWall;
    public AudioSource audioSourceFlap;
    public AudioSource audioSourceCircle;
    public AudioSource audioSourceHp;
    public AudioSource audioSourceRotation;
    public AudioSource audioSourceWarning;
    public GameObject deathEffect;
    public GameObject gameOver;
    public GameObject rotWarning;
    public GameObject tutorial;

    public float jumpForce = 10f;

    public float rotForce = 0f;
    public float rotAdd = 200f;

    public int hp = 3;
    public int hpMax = 5;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private ShakeManager shake;
    private ScoreManager score;

    private AudioClip wallImpactSound;
    private AudioClip flapSound;
    private AudioClip circleSound;
    private AudioClip hpSound;
    private AudioClip rotationSound;
    private AudioClip warningSound;

    private bool gravityStart = false;
    private bool gravityDown = true;

    private bool rotSurpass = false;

    private void Start()
    {
        rb.gravityScale *= 0;
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ShakeManager>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();

        wallImpactSound = (AudioClip)Resources.Load("wall_impact_sound");
        flapSound = (AudioClip)Resources.Load("flap_sound");
        circleSound = (AudioClip)Resources.Load("circle_sound");
        hpSound = (AudioClip)Resources.Load("hp_sound");
        rotationSound = (AudioClip)Resources.Load("rotation_sound");
        warningSound = (AudioClip)Resources.Load("rotation_warning_sound");
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotForce * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!gravityStart)
            {
                rb.gravityScale += 4;
                tutorial.SetActive(false);
                gravityStart = true;
            }
            if (!gravityDown)
            {
                rb.gravityScale *= -1;
                gravityDown = true;
            }
            audioSourceFlap.clip = flapSound;
            audioSourceFlap.Play();
            rb.velocity = Vector2.up * jumpForce;
            rotForce -= rotAdd;
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!gravityStart)
            {
                rb.gravityScale += 4;
                tutorial.SetActive(false);
                gravityStart = true;
            }
            if (gravityDown)
            {
                rb.gravityScale *= -1;
                gravityDown = false;
            }
            audioSourceFlap.clip = flapSound;
            audioSourceFlap.Play();
            rb.velocity = Vector2.down * jumpForce;
            rotForce += rotAdd;
        }
        if ((rotForce >= 1200 || rotForce <= -1200) && !rotSurpass)
        {
            rotWarning.SetActive(true);
            audioSourceWarning.clip = warningSound;
            audioSourceWarning.Play();
            rotSurpass = true;
        }
        if (rotForce < 1200 && rotForce > -1200 && rotSurpass)
        {
            rotWarning.SetActive(false);
            rotSurpass = false;
        }
        if (rotForce > 2000f)
        {
            hp -= 1;
            Debug.Log(hp);
            rotForce = 200f;
            audioSourceRotation.clip = rotationSound;
            audioSourceRotation.Play();
            shake.BumpShake();
        }
        if(rotForce < -2000f)
        {
            hp -= 1;
            Debug.Log(hp);
            rotForce = -200f;
            audioSourceRotation.clip = rotationSound;
            audioSourceRotation.Play();
            shake.BumpShake();
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < hpMax)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if(hp == 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            score.gameOver = true;
            gameOver.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TopLimit"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            audioSourceWall.clip = wallImpactSound;
            audioSourceWall.Play();
            shake.BumpShake();
            rb.velocity = Vector2.down * jumpForce * 2;
            hp -=  1;
        }
        else if (collision.CompareTag("BottomLimit"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            audioSourceWall.clip = wallImpactSound;
            audioSourceWall.Play();
            shake.BumpShake();
            rb.velocity = Vector2.up * jumpForce * 2;
            hp -= 1;
        }
        else if (collision.CompareTag("Circle"))
        {
            audioSourceCircle.clip = circleSound;
            audioSourceCircle.Play();
        }
        else if (collision.CompareTag("Hp")) 
        {
            audioSourceHp.clip = hpSound;
            audioSourceHp.Play();
        }
    }
}
