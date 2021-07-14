using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwap : MonoBehaviour
{
    private AbilityHandler abilityHandler;
    private Ability activeAbility;
    // Start is called before the first frame update
    void Start()
    {
        abilityHandler = GetComponent<AbilityHandler>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "AbilityPickup")
        {
            activeAbility = other.gameObject.GetComponent<AbilityPickupEntity>().GetStoredAbility();
            abilityHandler.ability = activeAbility;
            abilityHandler.abilityIndicator.sprite = activeAbility.icon;
        // spooky potion💀
            Destroy(other.gameObject);
        }
    }

}

