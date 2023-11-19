using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersMovement : MonoBehaviour
{
    public PlaneControllerV2 borderMovementSpeed;
    void Update()
    {
        transform.position += new Vector3(borderMovementSpeed.currentSpeed * Time.deltaTime, 0, 0);
    }
}
