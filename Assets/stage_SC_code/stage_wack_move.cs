using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_wack_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float speed = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
    }
}
