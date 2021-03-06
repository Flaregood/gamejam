using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL_OF_DEATH : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] GameObject player;
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x - this.transform.localScale.x-50, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
