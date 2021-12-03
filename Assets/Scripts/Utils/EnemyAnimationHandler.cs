using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private Vector2 LastPosition;
    private Animator anim;
    [SerializeField] private string CurrentState;
    private bool FacingRight;
    private bool Attacking;
    [SerializeField] private EnemyStats ScriptableObject;
    private SpriteRenderer SR;
    private string Idle;
    private string Move;
    private string Attack;
    private string Die;
    private string Hurt;
    private Transform Player;
    private bool Moving;
    private bool DieBool;

    void Start()
    {
        ScriptableObject = GetComponent<EnemyController>().stats;
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();

        Move = ScriptableObject.MoveAnim;
        Idle = ScriptableObject.IdleAnim;
        Attack = ScriptableObject.AttackAnim;
        Die = ScriptableObject.DieAnim;
        Hurt = ScriptableObject.HurtAnim;

    }
	private void FixedUpdate()
	{
        Moving = GetComponent<EnemyController>().Moving;
        Player = GameObject.Find("Player").GetComponent<Transform>();
        Attacking = GetComponent<EnemyController>().isAttacking;
        
        if(Moving == false && Attacking == false && DieBool == false)
		{
            ChangeAnimationState(Idle);
		}
        if(transform.position.x - Player.position.x < 0 && FacingRight == false && Moving == true && DieBool == false)
		{
            Flip();
            ChangeAnimationState(Move);
            // Debug.Log("Current Anim: " + CurrentState);
		}
        if(transform.position.x - Player.position.x > 0 && FacingRight == true && Moving == true && DieBool == false)
		{
            Flip();
            ChangeAnimationState(Move);
            // Debug.Log("Current Anim: " + CurrentState);

		}
        if (Attacking == true && DieBool == false)
        {
            ChangeAnimationState(Attack);
        }





	}
	private void Update()
	{
		
	}
	public void ChangeAnimationState(string NewState)
    {
        if (CurrentState == NewState) {
            Debug.Log("REPEATING ANIM!: c:" + CurrentState + "n: " + NewState );
            return;
            }
            
        anim.Play(NewState);
        CurrentState = NewState;
    }
    public void Flip()
    {
        FacingRight = !FacingRight;
        SR.flipX = !SR.flipX;

    }
    public void EnemyDie()
	{
        ChangeAnimationState(Die);
        DieBool = true;
	}
    public void EnemyHurt()
	{
        ChangeAnimationState(Hurt);
	}
}
