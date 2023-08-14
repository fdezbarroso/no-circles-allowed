using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject circle;
    public GameObject hp;

    public float speed;

    private void Start()
    {
        int rand = Random.Range(0, 20);
        if(rand <19)
        {
            var circlePreFab = (GameObject)Instantiate(circle, transform.position, Quaternion.identity);
            circlePreFab.GetComponent<Circle>().speed = speed;
        }
        else
        {
            var hpPreFab = (GameObject)Instantiate(hp, transform.position, Quaternion.identity);
            hpPreFab.GetComponent<Hp>().speed = speed;
        }
    }
}
