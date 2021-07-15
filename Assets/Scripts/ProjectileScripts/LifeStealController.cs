using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public string attackerTag;
    public float activeTime;
    public int damage;
    public float distanceSpeed;

    private Transform player;
    private GameObject[] enemys;

    private void Start()
    {
        player = transform.parent;
    }

    void Update()
    {
        DamageEnemys();

        activeTime -= Time.deltaTime;

        if (activeTime <= 0)
        {
            Color color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime * 2);
            spriteRenderer.color = color;

            if (color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void DamageEnemys()
    {

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
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Shield")
            {
                collision.gameObject.GetComponent<ShieldController>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
