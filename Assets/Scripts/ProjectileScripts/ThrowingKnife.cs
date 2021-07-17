using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.LookAt(clickPos);
        transform.Rotate(new Vector3(0,-90,0));

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
