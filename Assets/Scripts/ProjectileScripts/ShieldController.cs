using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Vector3 shieldOffset;
    public int shieldHealth;
    public float activeTime;

    private Transform player;

    private void Start()
    {
        player = PlayerController.instance.transform;
    }

    void Update()
    {
        Vector2 targetPos = player.position + shieldOffset;
        Vector2 target = Vector2.Lerp(transform.position, targetPos, 5 * Time.fixedDeltaTime);

        transform.position = new Vector3(target.x, target.y, transform.position.z);


        //transform.position = player.position + shieldOffset;

        activeTime -= Time.deltaTime;

        if (activeTime <= 0)
        {
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
