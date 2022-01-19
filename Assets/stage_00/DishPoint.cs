using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishPoint : MonoBehaviour
{
    public GameObject MyDish;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void AddFood(string addfood)
    {
        MyDish.SendMessage("AddFood", addfood);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
