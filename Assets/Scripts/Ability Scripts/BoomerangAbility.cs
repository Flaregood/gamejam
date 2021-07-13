using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoomerangAbility : Ability
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float distance;
    [SerializeField] private float spinSpeed;
    [SerializeField] private int amountProjectiles;

    public override void Activate(GameObject parent)
    {
        for (int i = 0; i < amountProjectiles; i++)
        {
            Quaternion projectileRotation = Quaternion.Euler(0, 0, (360f / amountProjectiles) * i); //Assign portion of 360° (circle) rotation for projectile
            GameObject newProjectile = Instantiate(original: projectile, position: parent.transform.position, rotation: projectileRotation, parent: parent.transform);
            newProjectile.GetComponent<BoomerangProjectileMovement>().distance_Speed = this.distance;
            newProjectile.GetComponent<BoomerangProjectileMovement>().spinningSpeed = this.spinSpeed;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }

}
