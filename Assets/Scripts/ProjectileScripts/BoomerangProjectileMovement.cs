using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileMovement : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private int damage;
    [SerializeField] public float distance_Speed;
    [SerializeField] public float spinningSpeed;
    public string attackerTag;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, spinningSpeed*360 * Time.fixedDeltaTime);
        transform.Translate(Vector3.right* Time.fixedDeltaTime* (distance_Speed*spinningSpeed));
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
                //collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                //Destroy(gameObject);
            }
        }
    }
}
