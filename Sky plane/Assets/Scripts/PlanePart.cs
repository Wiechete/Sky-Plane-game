using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePart : MonoBehaviour
{
    public Vector3 direction;
    public float explosionPower;


    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(direction * explosionPower, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));
    }
}
