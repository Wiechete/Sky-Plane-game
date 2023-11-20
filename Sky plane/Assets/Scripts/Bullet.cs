using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage;

    public float startingVelocity;

    public bool isEnemyBullet = false;

    private void Start()
    {        
        Destroy(gameObject, 2);
    }

    void FixedUpdate()
    {
        transform.position += transform.right * (bulletSpeed + startingVelocity) * Time.deltaTime;       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnemyBullet && collision.gameObject.TryGetComponent(out PlaneControllerV2 planeController)){
            planeController.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            if(!isEnemyBullet && collision.gameObject.TryGetComponent(out EnemyController enemyController)){
                enemyController.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

}
