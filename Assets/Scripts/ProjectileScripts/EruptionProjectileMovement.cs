using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EruptionProjectileMovement : MonoBehaviour
{
    public Vector2 centerPoint;

    [SerializeField] private int damage;
    [SerializeField] private float distanceSpeed;
    [SerializeField] private float spinningSpeed;
    public string attackerTag;

    private void Awake()
    {
        centerPoint = GameObject.Find("player").transform.position;
    }

    void FixedUpdate()
    {
        transform.RotateAround(point: centerPoint, axis: -Vector3.forward, angle: spinningSpeed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().velocity = (transform.right + transform.up) * distanceSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(attackerTag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
