using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Vector3 offset;

    public int shieldHealth;
    public float activeTime;

    private Transform player;

    private PlayerAnimations playerAnimations;

    private void Start()
    {
        player = PlayerController.instance.transform;
        healthBar.SetMaxHealth(shieldHealth);
        playerAnimations = player.GetComponent<PlayerAnimations>();
    }

    void FixedUpdate()
    {
        Vector3 adjustedOffset; 
        if (playerAnimations.FacingRight){
            adjustedOffset = transform.position + offset;
        } else {
             adjustedOffset = transform.position - offset;
        }
        
        Vector2 targetPos = Vector2.Lerp(adjustedOffset, player.position, 20 * Time.fixedDeltaTime);

        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);


        //transform.position = player.position;

        activeTime -= Time.deltaTime;

        if (activeTime <= 0)
        {
            if (spriteRenderer == null)
            {
                Destroy(gameObject);
                return;
            }

            Color color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime * 2);
            spriteRenderer.color = color;

            if (color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        shieldHealth -= damage;
        healthBar.SetHealth(shieldHealth);

        if (shieldHealth <= 0)
        {
            //TODO: Play death animation in here
            Destroy(gameObject);
        }
    }
}
