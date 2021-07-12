using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    private WeaponHandler weaponHandler;
    [SerializeField] private Weapon weapon1;
    [SerializeField] private Weapon weapon2;

    // Start is called before the first frame update
    void Start()
    {
        weaponHandler = GetComponent<WeaponHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        // ! This is for testing purposes only delete later
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponHandler.weapon = weapon1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponHandler.weapon = weapon2;

        }

    }
}
