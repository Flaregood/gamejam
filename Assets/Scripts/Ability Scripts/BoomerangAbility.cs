using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoomerangAbility : Ability
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private int amountProjectiles;

    public override void Activate(GameObject parent)
    {
        for (int i = 0; i < amountProjectiles; i++)
        {
            Quaternion projectileRotation = Quaternion.Euler(0, 0, (360f / amountProjectiles) * i); //Assign portion of 360° (circle) rotation for projectile
            GameObject newProjectile = Instantiate(original: projectile, position: parent.transform.position, rotation: projectileRotation, parent: parent.transform);

            newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.right * speed;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }

}
