using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Projectile
{
    private Rigidbody2D rb;

    void Start()
    {
        this.transform.LookAt(PlayerController.instance.transform.position);
        this.transform.Rotate(new Vector3(0, -90, 0));
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * distanceSpeed;
    }
}
