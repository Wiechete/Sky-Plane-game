using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float damage;
    
    void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
        //Debug.Log(transform.right * bulletSpeed * Time.deltaTime);
    }



}
