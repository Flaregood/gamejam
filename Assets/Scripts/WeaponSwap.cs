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
           activeWeapon =  other.gameObject.GetComponent<WeaponPickupEntity>().storedWeapon;
           weaponHandler.weapon = activeWeapon;
            // kabooom 💥
           Destroy(other.gameObject);
        }
    }

}
