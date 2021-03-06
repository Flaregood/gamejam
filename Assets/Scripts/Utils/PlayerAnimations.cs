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
    public bool FacingRight;
    private bool Attacking;

    #region Animations
    const string NormalAttackAnim = "Evelyn Attack";
    const string MoveAnim = "Evelyn Run";
    const string IdleAnim = "Idle Evelyn";
    const string DieAnim = "Evelyn Die";
	#endregion Animations
	void Start()
    {
        // rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
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

        if (Attacking == true)
		{
            ChangeAnimationState(NormalAttackAnim);
		}
        if (Movement != new Vector2(0,0) && Attacking == false)
		{
            ChangeAnimationState(MoveAnim);
		}
        if (Movement == new Vector2(0, 0) && Attacking == false)
		{
            ChangeAnimationState(IdleAnim);
		}
        if (FacingRight == true && Movement.x < 0)
        {
            Flip();
        }
        if (FacingRight == false && Movement.x > 0)
        {
            Flip();
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
    public void AnimationEnd()
    {
        Attacking = false;
    }
}
