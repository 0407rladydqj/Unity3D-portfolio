using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_wasd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float speed = 5f;

    void KeyWASD()
    { 
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed, 0, 0 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed, 0, 0 * Time.deltaTime);
        }
        /*
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate( 0, -speed, 0 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate( 0, speed, 0 * Time.deltaTime);
        }
        */
    }
    // Update is called once per frame
    void Update()
    {
        KeyWASD();
    }
}
