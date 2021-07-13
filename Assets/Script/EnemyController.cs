using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Ability ability;

    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 2;
    [SerializeField] private int followRadius = 6;
    [SerializeField] private int attackradius = 3;

    [SerializeField] private float attackCooldown = 2.5f;
    [SerializeField] private float attackTime = 1.5f;

    private float startAttackCooldown;

    private bool encounteredPlayer = false;
    private bool isAttacking = false;

    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(health);
        startAttackCooldown = attackCooldown;
    }

    private void FixedUpdate()
    {
        attackCooldown -= Time.deltaTime;

        float distance = Vector2.Distance(PlayerController.instance.transform.position, transform.position);

        if (distance < attackradius)
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
        transform.Translate((PlayerController.instance.transform.position - transform.position) * Time.fixedDeltaTime);
    }
}