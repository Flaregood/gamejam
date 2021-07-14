using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParentMovement : Projectile
{
    void Update()
    {
        transform.position = transform.position + (Vector3.right * distanceSpeed * Time.deltaTime);
    }
}
