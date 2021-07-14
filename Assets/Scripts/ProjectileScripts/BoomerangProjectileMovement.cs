using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileMovement : Projectile
{
    void FixedUpdate()
    {
        transform.Rotate(0, 0, spinningSpeed*360 * Time.fixedDeltaTime);
        transform.Translate(Vector3.right* Time.fixedDeltaTime* (distanceSpeed*spinningSpeed));
    }
}
