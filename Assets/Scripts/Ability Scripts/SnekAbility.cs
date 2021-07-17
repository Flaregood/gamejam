using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SnekAbility : Ability
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float distanceSpeed; 
    [SerializeField] private int amountProjectiles;
    [SerializeField] private int damage;

    public override void Activate(GameObject parent)
    {
            GameObject newProjectile = Instantiate(original: projectile, position: parent.transform.position,Quaternion.identity);

            newProjectile.GetComponent<RangedAttack>().distanceSpeed = distanceSpeed;
            newProjectile.GetComponent<RangedAttack>().attackerTag = parent.tag;
            newProjectile.GetComponent<RangedAttack>().activeTime = activeProjectileTime;
            newProjectile.GetComponent<RangedAttack>().damage = damage;
    }

    public override void BeginCooldown(GameObject parent)
    {

    }

}
