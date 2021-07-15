using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ForceFieldAbility : Ability
{
    [SerializeField] private GameObject forcefield;
    [SerializeField] private int shieldHealth;

    public override void Activate(GameObject parent)
    {
        GameObject newShield = Instantiate(original: forcefield, parent: parent.transform);

        Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), newShield.GetComponent<CircleCollider2D>());
        newShield.GetComponent<ShieldController>().activeTime = activeTime;
        newShield.GetComponent<ShieldController>().shieldHealth = shieldHealth;
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
