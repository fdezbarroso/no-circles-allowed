using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] circleSquads;

    public float iniSpawnTime = 2f;
    public float decSpawnTime = 0.03f;
    public float minSpawnTime = 0.52f;

    public float iniSpeed = 8f;
    public float addSpeed = 0.2f;
    public float maxSpeed = 22f;

    private float spawnTime;
    
    private void Update()
    {
        if(spawnTime <= 0)
        {
            int rand = Random.Range(0, circleSquads.Length);
            var circleSquad = Instantiate(circleSquads[rand], transform.position, Quaternion.identity);
            if (iniSpeed < maxSpeed)
            {
                iniSpeed += addSpeed;
            }
            //iniSpeed
            SpawnPoint[] circlesInSquad = circleSquad.GetComponentsInChildren<SpawnPoint>();
            
            foreach(SpawnPoint spawnable in circlesInSquad)
            {
                spawnable.GetComponent<SpawnPoint>().speed = iniSpeed;
            }

            spawnTime = iniSpawnTime;
            if(iniSpawnTime > minSpawnTime)
            {
                iniSpawnTime -= decSpawnTime;
            }
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }
}
