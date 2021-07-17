using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupEntity : MonoBehaviour
{
    [SerializeField] public Ability storedAbility;

// Spicy constructor for instenctiating for spell drops from enemy death.
    public AbilityPickupEntity(Ability ability){
        storedAbility = ability;
    }


    void Start()
    {
        gameObject.tag = "AbilityPickup";
        gameObject.GetComponent<SpriteRenderer>().sprite = storedAbility.icon;
        gameObject.GetComponentInChildren<TextMesh>().text = storedAbility.name;

    }

// Use to get currently stored ability by the power up (╯°□°）╯︵ ┻━┻
    public Ability GetStoredAbility(){
        return storedAbility;
    }


}
