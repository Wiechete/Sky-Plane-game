using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] islandsPrefabs;
    public GameObject[] rareIslandsPrefabs;
    public GameObject[] balloonsPrefabs;
    public GameObject[] enemyPlanePrefabs;
    public GameObject[] cloudsPrefabs;

    public Transform islandsParent;
    public Transform cloudsParent;
    public Transform balloonsParent;
    public Transform enemiesParent;

    List<GameObject> islands = new List<GameObject>();
    List<GameObject> clouds = new List<GameObject>();
    List<GameObject> balloons = new List<GameObject>();
    List<GameObject> enemyPlanes = new List<GameObject>();
    public PlaneControllerV2 planeController;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    public LayerMask islandLayerMask;
    public LayerMask balloonLayerMask;
    public LayerMask cloudsLayerMask;

    public float islandsCooldown;
    public float balloonsCooldown;
    public float enemyPlanesCooldown;
    public float cloudsCooldown;

    private void Start()
    {
        StartCoroutine("SpawnIslandsCoroutine");
        StartCoroutine("SpawnBalloonsCoroutine");
        StartCoroutine("SpawnEnemyPlanesCoroutine");
        StartCoroutine("SpawnCloudsCoroutine");
    }

    private void Update()
    {
        if (planeController == null) Destroy(gameObject);
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
    IEnumerator SpawnEnemyPlanesCoroutine()
    {
        yield return new WaitForSeconds(enemyPlanesCooldown);
        SpawnEnemyPlane();
        DespawnEnemyPlane();
        StartCoroutine("SpawnEnemyPlanesCoroutine");
    }
    IEnumerator SpawnCloudsCoroutine()
    {
        yield return new WaitForSeconds(cloudsCooldown);
        SpawnCloud();
        DespawnClouds();
        StartCoroutine("SpawnCloudsCoroutine");
    }

    void SpawnIsland()
    {
        if (planeController.transform.position.x < -5) return;
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = GetSpawnPosition();

            GameObject island = islandsPrefabs[Random.Range(0, islandsPrefabs.Length)];
            Vector2 size = island.GetComponent<BoxCollider2D>().size * island.transform.localScale;
            if (Physics2D.OverlapBox(position, size, 0, islandLayerMask + balloonLayerMask) == null)
            {
                islands.Add(Instantiate(island, transform.position + position, Quaternion.identity, islandsParent));
                break;
            }
        }
    }

    void SpawnBalloon()
    {
        if (planeController.transform.position.x < -5) return;
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = GetSpawnPosition();

            GameObject balloon = balloonsPrefabs[Random.Range(0, balloonsPrefabs.Length)];
            Vector2 size = balloon.GetComponent<BoxCollider2D>().size * balloon.transform.localScale;
            if (Physics2D.OverlapBox(position, size, 0, islandLayerMask + balloonLayerMask) == null)
            {
                balloons.Add(Instantiate(balloon, transform.position + position, Quaternion.identity, balloonsParent));
                break;
            }
        }
    }
    void SpawnEnemyPlane(){
        if (planeController.transform.position.x < 0) return;

        GameObject prefab = enemyPlanePrefabs[Random.Range(0, enemyPlanePrefabs.Length)];
        Vector3 position = new Vector3(planeController.transform.position.x - 10, Random.Range(0, 30));
        GameObject enemyPlane = Instantiate(prefab, enemiesParent);
        enemyPlane.transform.position = position;
        enemyPlane.GetComponent<EnemyController>().playerPlane = planeController;
        enemyPlanes.Add(enemyPlane);
    }
    void SpawnCloud()
    {
        if (planeController.transform.position.x < -5) return;
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = GetSpawnPosition();

            GameObject cloud = cloudsPrefabs[Random.Range(0, cloudsPrefabs.Length)];
            Vector2 size = cloud.GetComponent<BoxCollider2D>().size * cloud.transform.localScale;
            if (Physics2D.OverlapBox(position, size, 0, cloudsLayerMask) == null)
            {
                clouds.Add(Instantiate(cloud, transform.position + position, Quaternion.identity, cloudsParent));
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

    void DespawnEnemyPlane()
    {
        for (int i = 0; i < enemyPlanes.Count; i++)
        {
            GameObject plane = enemyPlanes[i];
            if (plane == null)
            {
                enemyPlanes.RemoveAt(i);
                continue;
            }
            if (plane.transform.position.x - planeController.transform.position.x > 30)
            {
                balloons.Remove(plane);
                Destroy(plane);
            }
        }
    }
    void DespawnClouds()
    {
        for (int i = 0; i < clouds.Count; i++)
        {
            GameObject cloud = clouds[i];
            if (cloud == null)
            {
                clouds.RemoveAt(i);
                continue;
            }
            if (planeController.transform.position.x - cloud.transform.position.x > 20)
            {
                clouds.Remove(cloud);
                Destroy(cloud);
            }
        }
    }
}
