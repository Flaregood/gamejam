using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    const string NormalAttackAnim = "NormalAttack";
    const string MoveAnimy1 = "Move+y";
    const string MoveAnimy2 = "Move-y";
    const string MoveAnimx1 = "Move+x";
    const string MoveAnimx2 = "Move-x";
    const string IdleAnim = "Idle";
    const string HurtAnim = "Hurt";
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
        if (Movement.x > 0 && Movement.y == 0 && Attacking == false)
		{
            ChangeAnimationState(MoveAnimx1);
		}
        if (Movement.x < 0 && Movement.y == 0 && Attacking == false)
        {
            ChangeAnimationState(MoveAnimx2);
        }
        if (Movement.x == 0 && Movement.y < 0 && Attacking == false)
        {
            ChangeAnimationState(MoveAnimy1);
        }
        if (Movement.x == 0 && Movement.y > 0 && Attacking == false)
        {
            ChangeAnimationState(MoveAnimy2);
        }
        else if (Attacking == true)
		{
            //call function to shoot from FEMI`s script
            ChangeAnimationState(NormalAttackAnim);
		}
        

    }
    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 180, 0);
    }



    public void HurtPlayer(int demage)
	{
        Health = Health - demage;
        ChangeAnimationState(HurtAnim);
	}

    public void ChangeAnimationState(string NewState)
    {
        if (CurrentState == NewState) return;
        anim.Play(NewState);
        CurrentState = NewState;
    }
}
