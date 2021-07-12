using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ThrowingKnifeWeapon : Weapon
{
    [SerializeField] private GameObject projectile;

    public override void Attack(GameObject firePoint)
    {
        Instantiate(original: projectile, position: firePoint.transform.position, rotation: firePoint.transform.rotation);
    }
}
