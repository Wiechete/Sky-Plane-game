using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform[] weaponsTr;
    [SerializeField] private Transform[] shootPositions;
    [SerializeField] private GameObject[] bulletPrefabs;

    [SerializeField] private float[] weaponsCooldowns;

    [SerializeField] private int currentWeapon = 0;

    [SerializeField] private Transform bulletsParent;

    private float lastTimeShoot = 0;

    private void Start()
    {
        UIManager.distanceUI.UpdateUI(0);
    }


    private void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) {
            Shoot();
        }
        if (transform.position.x >= -12.5f)
            UIManager.distanceUI.UpdateUI(Mathf.RoundToInt(transform.position.x + 12.5f));
    }

    private void Shoot(){
        if (Time.time - lastTimeShoot < weaponsCooldowns[currentWeapon]) return;
        lastTimeShoot = Time.time;
        GameObject bullet = Instantiate(bulletPrefabs[currentWeapon], bulletsParent);
        bullet.transform.position = shootPositions[currentWeapon].position;
        bullet.transform.right = transform.right;
        bullet.GetComponent<Bullet>().startingVelocity = Vector3.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);

        if(currentWeapon == 0) AudioManager.PlaySound(AudioManager.Sound.DefaultShoot);
        if(currentWeapon == 1) AudioManager.PlaySound(AudioManager.Sound.RifleShoot);
        if(currentWeapon == 2) AudioManager.PlaySound(AudioManager.Sound.RPGShoot);
    }

    public void ChangeWeapon(int index){

        weaponsTr[currentWeapon].gameObject.SetActive(false);
        currentWeapon = index;
        weaponsTr[currentWeapon].gameObject.SetActive(true);
    }
}
