using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    public Weapon weapon;

    public Image weaponIndicator; 
    [SerializeField] private GameObject firePoint;

    private float timeBetweenAttacks;

    private enum AttackState
    {
        ready,
        recovery
    }

    AttackState state = AttackState.ready;

    void Start(){
        weaponIndicator.sprite = weapon.icon;
    }

    void handleReady()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Attack(firePoint);
            state = AttackState.recovery;
            timeBetweenAttacks = weapon.timeBetweenAttacks;
        }
    }

    void handleRecovery()
    {
        if (timeBetweenAttacks > 0)
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
        else
        {
            state = AttackState.ready;
        }
    }

    void Update()
    {
        switch (state)
        {
            case AttackState.ready:
                handleReady();
                break;
            case AttackState.recovery:
                handleRecovery();
                break;
        }

    }
}
