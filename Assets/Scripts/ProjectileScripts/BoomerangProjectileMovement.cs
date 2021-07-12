using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float speed;

    private void Awake()
    {
        player = GameObject.Find("player");
    }

    void FixedUpdate()
    {
        transform.RotateAround(point: player.transform.position, axis: -transform.forward, angle: speed * Time.fixedDeltaTime);
    }
}
