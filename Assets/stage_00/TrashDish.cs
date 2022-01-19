using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDish : MonoBehaviour
{
    public GameObject SummonDish;
    public GameObject NewDish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dish")
        {
            Instantiate(NewDish, SummonDish.transform.position, SummonDish.transform.rotation);
        }
    }
    // Update is called once per frame

    void Update()
    {

    }
}
