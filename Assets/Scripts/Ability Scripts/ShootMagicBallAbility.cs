using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShootMagicBallAbility : Ability
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float movingSpeed; //The speed with wich the ability moves away from the player
    [SerializeField] private float distanceSpeed; //The speed with wich the projectiles moves away from the center
    [SerializeField] private float spinSpeed; //The speed with wich the projectiles spins around the center
    [SerializeField] private int amountProjectiles;
    [SerializeField] private int damage;

    public override void Activate(GameObject parent)
    {
        GameObject magicBallHolder = new GameObject();
        magicBallHolder.transform.position = parent.transform.position;
        //ProjectileParentMovement PPM = magicBallHolder.AddComponent(typeof(ProjectileParentMovement)) as ProjectileParentMovement;
        ProjectileParentMovement PPM = magicBallHolder.AddComponent(typeof(ProjectileParentMovement)) as ProjectileParentMovement;
        PPM.distanceSpeed = movingSpeed;

        for (int i = 0; i < amountProjectiles; i++)
        {
            Quaternion projectileRotation = Quaternion.Euler(0, 0, (360f / amountProjectiles) * i); //Assign portion of 360� (circle) rotation for projectile
            GameObject newProjectile = Instantiate(original: projectile, position: parent.transform.position, rotation: projectileRotation, parent: magicBallHolder.transform);

            newProjectile.GetComponent<ShootMagicBallMovement>().distanceSpeed = distanceSpeed;
            newProjectile.GetComponent<ShootMagicBallMovement>().spinningSpeed = spinSpeed;
            newProjectile.GetComponent<ShootMagicBallMovement>().attackerTag = parent.tag;
            newProjectile.GetComponent<ShootMagicBallMovement>().activeTime = activeProjectileTime;
            newProjectile.GetComponent<ShootMagicBallMovement>().damage = damage;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }

}
