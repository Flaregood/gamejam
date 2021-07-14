using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMagicBallMovement : Projectile
{
    void FixedUpdate()
    {
        transform.RotateAround(point: transform.parent.position, axis: -Vector3.forward, angle: spinningSpeed * Time.fixedDeltaTime);
        transform.position = transform.position + transform.up * Time.fixedDeltaTime;
    }
}
