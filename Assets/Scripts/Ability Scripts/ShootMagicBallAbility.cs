using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShootMagicBallAbility : Ability
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float distance;
    [SerializeField] private float spinSpeed;
    [SerializeField] private int amountProjectiles;

    public override void Activate(GameObject parent)
    {
        GameObject magicBallHolder = Instantiate(new GameObject(), position: parent.transform.position, Quaternion.identity);
        ThrowingKnife tk = magicBallHolder.AddComponent(typeof(ThrowingKnife)) as ThrowingKnife;

        for (int i = 0; i < amountProjectiles; i++)
        {
            Quaternion projectileRotation = Quaternion.Euler(0, 0, (360f / amountProjectiles) * i); //Assign portion of 360� (circle) rotation for projectile
            GameObject newProjectile = Instantiate(original: projectile, position: parent.transform.position, rotation: projectileRotation, parent: magicBallHolder.transform);
            newProjectile.GetComponent<ShootMagicBallMovement>().distanceSpeed = this.distance;
            newProjectile.GetComponent<ShootMagicBallMovement>().spinningSpeed = this.spinSpeed;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }

}
