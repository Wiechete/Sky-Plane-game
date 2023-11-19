using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBalloon : MonoBehaviour
{
    public int HPAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlaneControllerV2 planeController))
        {
            planeController.AddHP(HPAmount);
            Destroy(gameObject);
        }

    }
}
