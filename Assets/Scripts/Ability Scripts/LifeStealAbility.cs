using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LifeStealAbility : Ability
{
    [SerializeField] private GameObject forcefield;
    [SerializeField] private int shieldHealth;

    public override void Activate(GameObject parent)
    {
        GameObject lifeStealCircle = Instantiate(original: forcefield, parent: parent.transform);

        lifeStealCircle.GetComponent<ShieldController>().activeTime = activeTime;
        lifeStealCircle.GetComponent<ShieldController>().shieldHealth = shieldHealth;
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}

