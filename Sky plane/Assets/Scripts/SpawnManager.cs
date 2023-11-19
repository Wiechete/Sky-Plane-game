using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] islandsPrefabs;
    public GameObject[] balloonsPrefabs;

    public Transform islandsParent;
    public Transform balloonsParent;

    List<GameObject> islands = new List<GameObject>();
    List<GameObject> balloons = new List<GameObject>();
    public PlaneControllerV2 planeController;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    public LayerMask layerMask;

    public float islandsCooldown;
    public float balloonsCooldown;

    private void Start()
    {
        //for (int i = 0; i < 5; i++) SpawnIsland();
        StartCoroutine("SpawnIslandsCoroutine");
        StartCoroutine("SpawnBalloonsCoroutine");
    }

    IEnumerator SpawnIslandsCoroutine()
    {
        yield return new WaitForSeconds(islandsCooldown);
        SpawnIsland();
        DespawnIslands();
        StartCoroutine("SpawnIslandsCoroutine");
    }

    IEnumerator SpawnBalloonsCoroutine()
    {
        yield return new WaitForSeconds(balloonsCooldown);
        SpawnBalloon();
        DespawnBalloons();
        StartCoroutine("SpawnBalloonsCoroutine");
    }

    void SpawnIsland()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = GetSpawnPosition();

            GameObject island = islandsPrefabs[Random.Range(0, islandsPrefabs.Length)];
            Vector2 size = island.GetComponent<BoxCollider2D>().size * island.transform.localScale;
            if (Physics2D.OverlapBox(position, size, 0, layerMask) == null)
            {
                islands.Add(Instantiate(island, transform.position + position, Quaternion.identity, islandsParent));
                break;
            }
        }
    }

    void SpawnBalloon()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = GetSpawnPosition();

            GameObject balloon = balloonsPrefabs[Random.Range(0, balloonsPrefabs.Length)];
            Vector2 size = balloon.GetComponent<BoxCollider2D>().size * balloon.transform.localScale;
            if (Physics2D.OverlapBox(position, size, 0, layerMask) == null)
            {
                balloons.Add(Instantiate(balloon, transform.position + position, Quaternion.identity, balloonsParent));
                break;
            }
        }
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 position = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
        position += planeController.transform.position;
        if (position.y < -6) position.y = -6 + Random.Range(0, 30);
        return position;
    }

    void DespawnIslands(){
        for(int i = 0; i < islands.Count; i++){
            GameObject island = islands[i];
            if (island == null)
            {
                islands.RemoveAt(i);
                continue;
            }
            if (planeController.transform.position.x - island.transform.position.x > 20) {
                islands.Remove(island);
                Destroy(island);                            
            }            
        }
    }

    void DespawnBalloons(){
        for (int i = 0; i < balloons.Count; i++)
        {
            GameObject balloon = balloons[i];
            if (balloon == null)
            {
                balloons.RemoveAt(i);
                continue;
            }
            if (planeController.transform.position.x - balloon.transform.position.x > 20)
            {
                balloons.Remove(balloon);
                Destroy(balloon);                
            }
        }
    }
}
