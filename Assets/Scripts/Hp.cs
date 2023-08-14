using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject healEffect;

    public int heal = 1;
    public float speed;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            if(collision.GetComponent<Square>().hp < collision.GetComponent<Square>().hpMax)
            {
                collision.GetComponent<Square>().hp += heal;
                Debug.Log(collision.GetComponent<Square>().hp);
            }
            Instantiate(healEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
