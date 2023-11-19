using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform[] weaponsTr;
    [SerializeField] private Transform[] shootPositions;
    [SerializeField] private GameObject[] bulletPrefabs;

    [SerializeField] private int currentWeapon = 0;

    [SerializeField] private Transform bulletsParent;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

    private void Shoot(){
        GameObject bullet = Instantiate(bulletPrefabs[currentWeapon], bulletsParent);
        bullet.transform.position = shootPositions[currentWeapon].position;
        bullet.transform.right = transform.right;
        bullet.GetComponent<Bullet>().startingVelocity = Vector3.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    public void ChangeWeapon(int index){

        weaponsTr[currentWeapon].gameObject.SetActive(false);
        currentWeapon = index;
        weaponsTr[currentWeapon].gameObject.SetActive(true);
    }
}