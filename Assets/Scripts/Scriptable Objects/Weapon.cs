using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public new string name;
    public float timeBetweenAttacks;

    public Sprite icon;


    public virtual void Attack(GameObject firePoint) {}

}
