using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBox : MonoBehaviour
{
    public GameObject SummonFood;
    public GameObject ForSummonFood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Summon()
    {
        Instantiate(ForSummonFood, SummonFood.transform.position, SummonFood.transform.rotation);
    }

    void Update()
    {
        
    }
}
