using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject lifeStealParticlePrefab;

    public string attackerTag;
    public float activeTime;
    public int damage;
    public float distanceSpeed;
    public float damageTick;
    public float startDamageTick;

    private Transform player;
    public List<GameObject> enemys = new List<GameObject>();

    private void Start()
    {
        player = transform.parent;
        startDamageTick = damageTick;
    }

    void Update()
    {
        activeTime -= Time.deltaTime;
        damageTick -= Time.deltaTime;

        if (damageTick <= 0)
        {
            damageTick = startDamageTick;
            DamageEnemys();
        }

        if (activeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageEnemys()
    {
        List<GameObject> killedEnemys = new List<GameObject>();

        for (int i = 0; i < enemys.Count; i++)
        {
            if (attackerTag == "Player")
            {
                enemys[i].GetComponent<EnemyController>().TakeDamage(damage);
                transform.parent.GetComponent<PlayerController>().TakeDamage(-damage);
            }
            else
            {
                enemys[i].GetComponent<PlayerController>().TakeDamage(damage);
                transform.parent.GetComponent<EnemyController>().TakeDamage(-damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackerTag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemys.Add(collision.gameObject);
                //AdLifeStealParticles(collision.transform); //Use this function if particles should fly from the enemy to the player
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                enemys.Add(collision.gameObject);
                //AdLifeStealParticles(collision.transform); //Use this function if particles should fly from the player to the enemy
            }
        }
    }    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (attackerTag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemys.Remove(collision.gameObject);
                RemoveLifeStealParticles(collision.transform);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                enemys.Remove(collision.gameObject);
                RemoveLifeStealParticles(collision.transform);
            }
        }
    }

    private void AdLifeStealParticles(Transform parent)
    {
        Instantiate(original: lifeStealParticlePrefab, parent: parent);
    }

    private void RemoveLifeStealParticles(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name == "LifeStealParticleSystem(Clone)")
            {
                Destroy(parent.GetChild(i).gameObject);
                return;
            }
        }
    }
}
