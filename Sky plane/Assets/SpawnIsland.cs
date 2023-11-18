using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIsland : MonoBehaviour
{
    public GameObject[] islandsPrefabs;
    List<GameObject> islands = new List<GameObject>();
    public PlaneControllerV2 planeController;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    public LayerMask layerMask;

    public float timeBetweenSpawn;

    private void Start()
    {
        for (int i = 0; i < 5; i++) Spawn();
        StartCoroutine("SpawnCoroutine");
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        Spawn();
        StartCoroutine("SpawnCoroutine");
    }

    void Spawn()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
            position += planeController.transform.position;
            if (position.y < -6) position.y = -6 + Random.Range(0, 30);

            GameObject island = islandsPrefabs[Random.Range(0, islandsPrefabs.Length)];
            Vector2 size = island.GetComponent<BoxCollider2D>().size * island.transform.localScale;
            if (Physics2D.OverlapBox(position, size, 0, layerMask) == null)
            {
                islands.Add(Instantiate(island, transform.position + position, Quaternion.identity));
                break;
            }
        }
    }
}
