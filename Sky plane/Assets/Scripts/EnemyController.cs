using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject planeWarningPrefab;
    private GameObject planeWarning;

    public float planeSpeed;
    public int HP;
    public GameObject bulletPrefab;
    public float shootingCooldown;

    public PlaneControllerV2 playerPlane;

    public Transform shootingPoint;

    [SerializeField] private GameObject explosion;
    bool isAlreadySpawned = false;
    Vector3 movementBeforeSpawn = Vector3.zero;
    private void Start()
    {
        StartCoroutine("SpawnPlane");
    }

    IEnumerator SpawnPlane()
    {
        planeWarning = Instantiate(planeWarningPrefab);
        planeWarning.transform.position = Camera.main.transform.position - new Vector3(18.25f, 0, 0); 
        planeWarning.transform.position = new Vector3(planeWarning.transform.position.x, transform.position.y, 0);
        
        yield return new WaitForSeconds(1);
        Destroy(planeWarning);
        isAlreadySpawned = true;
        transform.position += movementBeforeSpawn;
        if (bulletPrefab != null)
            StartCoroutine("Shoot");
    }

    void FixedUpdate()
    {
        if(isAlreadySpawned)
            transform.position += Vector3.right * Time.fixedDeltaTime * (planeSpeed + playerPlane.currentSpeed / 2.5f);
        else{
            planeWarning.transform.position += Vector3.right * playerPlane.currentSpeed * Time.deltaTime;
            movementBeforeSpawn += Vector3.right * playerPlane.currentSpeed * Time.deltaTime;
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootingCooldown);
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = shootingPoint.position;
        bullet.GetComponent<Bullet>().startingVelocity = planeSpeed + playerPlane.currentSpeed / 2.5f;
        StartCoroutine("Shoot");
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP < 0) Explode();
    }

    public void Explode()
    {
        PlaneControllerV2.planesDestroyed++;
        GameObject expl = Instantiate(explosion);
        expl.transform.position = transform.position;
        Destroy(expl, 1);
        Destroy(gameObject);
    }
}
