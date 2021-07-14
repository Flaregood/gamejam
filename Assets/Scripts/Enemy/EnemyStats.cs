using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public Ability ability;
    public Sprite enemySprite;

    public int health; //Damage-Points till the enemy dies
    public float speed; //Movement speed
    public int followRadius; //If Player enters this radius, the enemy will follow
    public int attackRadius; //If Player enters this radius, the enemy will attack

    public float attackCooldown; //Minimum time between two attacks
    public float attackTime; //Seconds till the combat animation uses the ability
}
