using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EnemyStats stats;

    private Ability ability;
    private int health;
    private float speed;
    private int followRadius;
    private int attackRadius;

    private float attackTime; //Seconds till the combat animation uses the ability

    private float attackCooldown; //Minimum time between two attacks
    private float startAttackCooldown;

    private bool encounteredPlayer = false; //Was the player ever near enough for the enemy to follow him
    private bool isAttacking = false; //Is the enemy currently attacking

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = stats.enemySprite;

        ability = stats.ability;
        health = stats.health;
        speed = stats.speed;
        followRadius = stats.followRadius;
        attackRadius = stats.attackRadius;
        attackTime = stats.attackTime;
        attackCooldown = stats.attackCooldown;
        startAttackCooldown = stats.attackCooldown;

        healthBar.SetMaxHealth(stats.health);
    }

    private void FixedUpdate()
    {
        attackCooldown -= Time.deltaTime;

        float distance = Vector2.Distance(PlayerController.instance.transform.position, transform.position);

        if (distance < attackRadius)
        {
            if (attackCooldown <= 0)
            {
                StartCoroutine(Attack());
                attackCooldown = startAttackCooldown;
            }
        }
        else if ((distance < followRadius || encounteredPlayer == true) && isAttacking == false)
        {
            encounteredPlayer = true;

            FollowPlayer();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            //TODO: Play death animation in here
            Destroy(gameObject);
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(attackTime);

        ability.Activate(gameObject);

        //Shortrange = if still in radius after attack animation, make damage
        //Longrange = shoot at position the player is on at the moment he fires

        isAttacking = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void FollowPlayer()
    {
        transform.Translate((PlayerController.instance.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime);
    }
}
