using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSound : MonoBehaviour
{
    bool a = true;
    public void MuteAllSound()
    {
        if (a == true)
        {
            AudioListener.volume = 0;
            a = false;
        }
        else
        {
            AudioListener.volume = 1;
            a = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
