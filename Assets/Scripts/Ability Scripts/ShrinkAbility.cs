using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShrinkAbility : Ability
{
    public float shrinkFactor;
    private Vector3 originalScale;

    public override void Activate(GameObject parent)
    {
        originalScale = parent.transform.localScale;
        parent.transform.localScale = new Vector3(originalScale.x - shrinkFactor, originalScale.y - shrinkFactor, originalScale.z - shrinkFactor);

    }

    public override void BeginCooldown(GameObject parent)
    {
        parent.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
    }

}
