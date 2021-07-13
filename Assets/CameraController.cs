using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x > this.transform.position.x)
        {
            //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            Vector2 target = Vector2.Lerp(transform.position, player.transform.position, 2 * Time.fixedDeltaTime);
            transform.position = new Vector3(target.x, target.y, transform.position.z);
        }
        else
        {
            //this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);

            Vector2 targetPos = new Vector2(transform.position.x, player.transform.position.y);
            Vector2 target = Vector2.Lerp(transform.position, targetPos, 2 * Time.fixedDeltaTime);

            transform.position = new Vector3(target.x, target.y, transform.position.z);
        }
    }
}