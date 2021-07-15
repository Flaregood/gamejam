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
    private string Idle;
    private string Move;
    private string Attack;

    void Start()
    {
        anim = GetComponent<Animator>();
        Idle = "";
        Move = "";
        Attack = "";
    }
	private void FixedUpdate()
	{
        if(transform.position.x - LastPosition.x == 0 && Attacking == false)
		{
            ChangeAnimationState(Idle);
		}
        if(LastPosition.x - transform.position.x > 0 && FacingRight)
		{
            Flip();
            ChangeAnimationState(Move);
		}
        if(transform.position.x - LastPosition.x < 0 && FacingRight == false)
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
    public void ChangeAnimationState(string NewState)
    {
        if (CurrentState == NewState) return;
        anim.Play(NewState);
        CurrentState = NewState;
    }
    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 180, 0);
    }

}
