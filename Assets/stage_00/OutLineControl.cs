using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LineOnOff()
    {
        StopCoroutine("LineOnTime");
        StartCoroutine("LineOnTime");
    }

    IEnumerator LineOnTime()
    {
        gameObject.GetComponent<Outline>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
