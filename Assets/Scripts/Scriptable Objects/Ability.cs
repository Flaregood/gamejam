using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a template class to be used to create new abilities.
public class Ability : ScriptableObject
{
    public new string name; 
    public float cooldownTime;
    public float activeTime;
    public float activeProjectileTime;

    public Sprite icon;

//  To be overridden by base ability classes
    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) {}
}
