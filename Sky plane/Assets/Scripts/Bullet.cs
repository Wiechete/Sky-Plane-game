using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float damage;

    public float startingVelocity;

    private void Start()
    {        
        Destroy(gameObject, 2);
    }

    void FixedUpdate()
    {
        transform.position += transform.right * (bulletSpeed + startingVelocity) * Time.deltaTime;
        
        
    }



}
