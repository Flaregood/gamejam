using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupEntity : MonoBehaviour
{
    [SerializeField] public Weapon storedWeapon;

    public WeaponPickupEntity(Weapon weapon){
        storedWeapon = weapon;
    }

    void Start()
    {
        gameObject.tag = "WeaponPickup";
        gameObject.GetComponent<SpriteRenderer>().sprite = storedWeapon.icon;
        gameObject.GetComponentInChildren<TextMesh>().text = storedWeapon.name;
    }

    public Weapon GetStoredWeapon(){
        return storedWeapon;
    }


}
