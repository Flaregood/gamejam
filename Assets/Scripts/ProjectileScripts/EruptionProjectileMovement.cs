using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EruptionProjectileMovement : MonoBehaviour
{
    private Vector2 centerPoint;

    [SerializeField] private float distanceSpeed;
    [SerializeField] private float spinningSpeed;

    private void Awake()
    {
        centerPoint = GameObject.Find("player").transform.position;
    }

    void FixedUpdate()
    {
        transform.RotateAround(point: centerPoint, axis: -Vector3.forward, angle: spinningSpeed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().velocity = (transform.right + transform.up) * distanceSpeed * Time.fixedDeltaTime;
    }
}
