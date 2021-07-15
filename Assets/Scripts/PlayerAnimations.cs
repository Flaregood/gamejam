using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Vector2 Movement;
    public float Speed;
    private Rigidbody2D rb;
    private Animator anim;
    private string CurrentState;
    private bool FacingRight;
    [SerializeField] private int Health;
    private bool Attacking;

    #region Animations
    const string NormalAttackAnim = "NormalAttack_Player";
    const string MoveAnim = "Move_Player";
    const string IdleAnim = "Idle_Player";
    const string HurtAnim = "Hurt_Player";
    const string DieAnim = "Die_Player";
	#endregion Animations
	void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FacingRight = true;
    }

    
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
        rb.velocity = Movement * Speed * Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Attacking = true;
        }
        if (FacingRight == true && Movement.x < 0)
        {
            Flip();
        }
        if (FacingRight == false && Movement.x > 0)
        {
            Flip();
        }
        if (Attacking == true)
        {
            //call function to shoot from FEMI`s script
            ChangeAnimationState(NormalAttackAnim);
        }
        else if (Movement != new Vector2(0,0) && Attacking == false)
		{
            ChangeAnimationState(MoveAnim);
		}
        else if (Movement == new Vector2(0, 0))
		{
            ChangeAnimationState(IdleAnim);
		}

        else if (Attacking == true)
		{
            ChangeAnimationState(NormalAttackAnim);
		}

        

    }
    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 180, 0);
    }

    public void Die()
	{
        ChangeAnimationState(DieAnim);
	}



    public void ChangeAnimationState(string NewState)
    {
        if (CurrentState == NewState) return;
        anim.Play(NewState);
        CurrentState = NewState;
    }

}
