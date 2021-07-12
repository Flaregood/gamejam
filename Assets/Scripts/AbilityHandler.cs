using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public Ability ability;
    private float cooldownTime;
    private float activeTime;

    private enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    private void handleReady()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && transform.tag == "Player")
        {
            // Call activate from the Ability template class, set ability state to active.
            ability.Activate(gameObject); // call Activate on the object the ability handler script is on.
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }
    }

    private void handleActive()
    {
        // When ability is active begin countdown then set state to cooldown.
        if (activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }
        else
        {
            ability.BeginCooldown(gameObject);
            state = AbilityState.cooldown;
            cooldownTime = ability.cooldownTime;
        }
    }

    private void handleCooldown()
    {
        // When ability on cooldown begin countdown then set state to ready.
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }
        else
        {
            state = AbilityState.ready;
        }
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                handleReady();
                break;
            case AbilityState.active:
                handleActive();
                break;
            case AbilityState.cooldown:
                handleCooldown();
                break;

        }

    }
}