using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeAbility : Ability
{
    [SerializeField] private int damage;
    private float attackRange;

    public override void Activate(GameObject parent)
    {
        attackRange = parent.GetComponent<EnemyController>().attackRadius;
        Vector2 pos = new Vector2(parent.transform.position.x, parent.transform.position.y);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, attackRange);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject target = hitColliders[i].gameObject;

            if (target.tag == "Player")
            {
                target.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                return;
            }
            else if (target.gameObject.tag == "Shield")
            {
                target.gameObject.GetComponent<ShieldController>().TakeDamage(damage);
                return;
            }
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
