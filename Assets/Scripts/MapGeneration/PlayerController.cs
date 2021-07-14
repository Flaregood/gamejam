using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody2D body;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int health;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        body = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y) + new Vector2(horizontal * runSpeed, vertical * runSpeed), runSpeed * Time.fixedDeltaTime);
        //transform.Translate(new Vector2(horizontal * runSpeed, vertical * runSpeed) * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            //TODO: Play death animation in here
            gameObject.SetActive(false);
        }
    }
}
