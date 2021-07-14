using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string attackerTag;
    public float activeTime;
    public int damage;
    public float distanceSpeed;
    public float spinningSpeed;
    public Vector2 centerPoint;


    private void Update()
    {
        activeTime -= Time.deltaTime;

        if (activeTime <= 0)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime * 2);
            GetComponent<SpriteRenderer>().color = color;

            if (color.a <= 0)
                Destroy(gameObject);
        }
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
                //collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                //Destroy(gameObject);
            }
        }
    }
}
