using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody2D body;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Dialog[] deathDialogs;
    [SerializeField] private Dialog[] responseDialogs;
    private DialogController dialogController;

    [SerializeField] public int health;
    private int maxHealth;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        maxHealth = health;
        body = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(health);
        dialogController = gameObject.GetComponentInChildren<DialogController>();

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

            DisablePlayer();
            GetComponent<PlayerAnimations>().Die();
            DeathDialog();
            StartCoroutine(RestartScene());



        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void DeathDialog()
    {
        Dialog randomDialog = deathDialogs[Random.Range(0, deathDialogs.Length)];
        Dialog randomResponse = responseDialogs[Random.Range(0, responseDialogs.Length)];

        StartCoroutine(dialogController.StartDialog(randomDialog, dialogController.StartDialog(randomResponse)));
    }

    void DisablePlayer()
    {
        this.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<AbilityHandler>().enabled = false;
        gameObject.GetComponent<WeaponHandler>().enabled = false;
        gameObject.GetComponent<PlayerAnimations>().enabled = false;
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainGame");

    }
}
