using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBalloon : MonoBehaviour
{
    public int fuelAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlaneControllerV2 planeController))
        {
            planeController.AddFuel(fuelAmount);
            Destroy(gameObject);
        }
            
    }
}
