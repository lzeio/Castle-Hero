using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x >= -14)
                transform.position += new Vector3(-0.08f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x <= 14)
                transform.position += new Vector3(0.08f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y <= 7)
                transform.position += new Vector3(0, 0f, 0.08f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y >= -7)
                transform.position += new Vector3(0, 0f, -0.08f);
        }

       // FindObjectOfType<AudioManager>().play("1");
    }
}
