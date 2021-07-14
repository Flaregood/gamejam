using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldAbility : Ability
{
    [SerializeField] private GameObject shield;
    [SerializeField] private int shieldHealth;

    public override void Activate(GameObject parent)
    {
        Debug.Log("CreateShield");
        GameObject newShield = Instantiate(original: shield, parent: parent.transform);

        newShield.GetComponent<ShieldController>().activeTime = activeTime;
        newShield.GetComponent<ShieldController>().shieldHealth = shieldHealth;

    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
