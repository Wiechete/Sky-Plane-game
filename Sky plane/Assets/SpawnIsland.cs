using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIsland : MonoBehaviour
{
    public GameObject Island;
    public PlaneControllerV2 islandMovementSpeed;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;


    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(islandMovementSpeed.currentSpeed * Time.deltaTime, 0, 0);
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }
    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Instantiate(Island, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
