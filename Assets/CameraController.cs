using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > this.transform.position.x)
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
        }
    }
}