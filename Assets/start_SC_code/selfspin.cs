using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfspin: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float turnSpeed = 50;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
    }
}
