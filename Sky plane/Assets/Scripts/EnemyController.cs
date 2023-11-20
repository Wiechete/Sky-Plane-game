using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float planeSpeed;
    public int HP;
    public GameObject bulletPrefab;
    public float shootingCooldown;

    public PlaneControllerV2 playerPlane;

    public Transform shootingPoint;
    private void Awake()
    {
        if (bulletPrefab != null)
            StartCoroutine("Shoot");
    }

    void FixedUpdate()
    {
        //rb.MovePosition(transform.position + Vector3.right * Time.fixedDeltaTime * (planeSpeed + playerPlane.currentSpeed / 2.5f));
        transform.position += Vector3.right * Time.fixedDeltaTime * (planeSpeed + playerPlane.currentSpeed / 2.5f);
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
        Destroy(gameObject);
    }
}
