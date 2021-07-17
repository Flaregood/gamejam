using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LifeStealAbility : Ability
{
    [SerializeField] private GameObject lifeSteal;
    [SerializeField] private GameObject lifeStealParticlePrefab;
    [SerializeField] private int damage;
    [SerializeField] private int distanceSpeed;
    [SerializeField] private float damageTick;

    public override void Activate(GameObject parent)
    {
        GameObject lifeStealCircle = Instantiate(original: lifeSteal, parent: parent.transform);

        lifeStealCircle.GetComponent<LifeStealController>().lifeStealParticlePrefab = lifeStealParticlePrefab;
        lifeStealCircle.GetComponent<LifeStealController>().attackerTag = parent.tag;
        lifeStealCircle.GetComponent<LifeStealController>().activeTime = activeTime;
        lifeStealCircle.GetComponent<LifeStealController>().damage = damage;
        lifeStealCircle.GetComponent<LifeStealController>().damageTick = damageTick;
        lifeStealCircle.GetComponent<LifeStealController>().distanceSpeed = distanceSpeed;
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}

