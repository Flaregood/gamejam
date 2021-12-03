using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    private WeaponHandler weaponHandler;
    private Weapon activeWeapon;


    // Start is called before the first frame update
    void Start()
    {
        weaponHandler = GetComponent<WeaponHandler>();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "WeaponPickup")
        {
           activeWeapon =  other.gameObject.GetComponent<WeaponPickupEntity>().GetStoredWeapon();
           weaponHandler.weapon = activeWeapon;
           weaponHandler.weaponIndicator.sprite = activeWeapon.icon;
            // kabooom 💥
           Destroy(other.gameObject);
        }
    }

}
