using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOnIt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "FoodOnPoint")
        {
            gameObject.transform.position = other.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
