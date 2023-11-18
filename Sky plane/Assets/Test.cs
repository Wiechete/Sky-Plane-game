using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Physics2D.OverlapBox(transform.position, transform.localScale,0 , layerMask));
    }
}
