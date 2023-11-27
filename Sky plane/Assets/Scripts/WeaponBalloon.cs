using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBalloon : MonoBehaviour
{
    public int weaponIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WeaponController weaponController))
        {
            AudioManager.PlaySound(AudioManager.Sound.BalloonPickup);
            weaponController.ChangeWeapon(weaponIndex);
            Destroy(gameObject);
        }

    }
}
