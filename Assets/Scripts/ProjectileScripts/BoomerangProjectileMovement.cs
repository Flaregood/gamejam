using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private int damage;
    [SerializeField] private float speed;
    public string attackerTag;

    private void Awake()
    {
        player = GameObject.Find("player");
    }

    void FixedUpdate()
    {
        transform.RotateAround(point: player.transform.position, axis: -transform.forward, angle: speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackerTag == "Player")
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
