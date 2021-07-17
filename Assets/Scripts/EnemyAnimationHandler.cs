using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private Vector2 LastPosition;
    private Animator anim;
    private string CurrentState;
    private bool FacingRight;
    private bool Attacking;
    private EnemyStats ScriptableObject;
    private string Idle;
    private string Move;
    private string Attack;
    private string Die;
    private string Hurt;

    void Start()
    {
        ScriptableObject = GetComponent<EnemyController>().stats;
        anim = ScriptableObject.anim;


        Move = ScriptableObject.MoveAnim;
        Idle = ScriptableObject.IdleAnim;
        Attack = ScriptableObject.AttackAnim;
        Die = ScriptableObject.DieAnim;
        Hurt = ScriptableObject.HurtAnim;

    }
	private void FixedUpdate()
	{
        Attacking = GetComponent<EnemyController>().isAttacking;
        
        if(transform.position.x - LastPosition.x == 0 && Attacking == false)
		{
            ChangeAnimationState(Idle);
		}
        if(LastPosition.x - transform.position.x > 0 && FacingRight)
		{
            Flip();
            ChangeAnimationState(Move);
		}
        if(transform.position.x - LastPosition.x < 0 && FacingRight == true)
		{
            Flip();
            ChangeAnimationState(Move);
		}
		else if (transform.position.x - LastPosition.x != 0)
		{

		}
        if (transform.position.x - LastPosition.x == 0 && Attacking == true)
        {
            ChangeAnimationState(Attack);
        }


        LastPosition = transform.position;


	}
	private void Update()
	{
		
	}
	public void ChangeAnimationState(string NewState)
    {
        if (CurrentState == NewState) return;
        anim.Play(NewState);
        CurrentState = NewState;
    }
    public void Flip()
    {
        FacingRight = !FacingRight;
    }
    public void EnemyDie()
	{
        ChangeAnimationState(Die);
	}
    public void EnemyHurt()
	{
        ChangeAnimationState(Hurt);
	}
}
