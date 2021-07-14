using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoomerangAbility : Ability
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float distanceSpeed; //The speed with wich the projectiles moves away from the center
    [SerializeField] private float spinSpeed; //The speed with wich the projectiles spins around the center
    [SerializeField] private int amountProjectiles;
    [SerializeField] private int damage;

    public override void Activate(GameObject parent)
    {
        for (int i = 0; i < amountProjectiles; i++)
        {
            Quaternion projectileRotation = Quaternion.Euler(0, 0, (360f / amountProjectiles) * i); //Assign portion of 360� (circle) rotation for projectile
            GameObject newProjectile = Instantiate(original: projectile, position: parent.transform.position, rotation: projectileRotation, parent: parent.transform);

            newProjectile.GetComponent<BoomerangProjectileMovement>().distanceSpeed = distanceSpeed;
            newProjectile.GetComponent<BoomerangProjectileMovement>().spinningSpeed = spinSpeed;
            newProjectile.GetComponent<BoomerangProjectileMovement>().attackerTag = parent.tag;
            newProjectile.GetComponent<BoomerangProjectileMovement>().activeTime = activeProjectileTime;
            newProjectile.GetComponent<BoomerangProjectileMovement>().damage = damage;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }


   

}
