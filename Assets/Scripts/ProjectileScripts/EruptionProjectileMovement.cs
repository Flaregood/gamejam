using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EruptionProjectileMovement : Projectile
{
    private void Awake()
    {
        centerPoint = PlayerController.instance.transform.position;
    }

    void FixedUpdate()
    {
        transform.RotateAround(point: centerPoint, axis: -Vector3.forward, angle: spinningSpeed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().velocity = (transform.right + transform.up) * distanceSpeed * Time.fixedDeltaTime;
    }
}
