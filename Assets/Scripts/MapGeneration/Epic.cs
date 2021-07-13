using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epic : MonoBehaviour
{
    // Start is called before the first frame update
    float velocity = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.forward * velocity;
    }
}
