using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakMoveIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float speed = 12f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0f, 270f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0f, 225f, 0f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0f, 135f, 0f);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0f, 45f, 0f);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0f, 315f, 0f);
        }
    }
}
