using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour 
{
    void Update()
    {
        if(gameObject.tag == "Clockwise"){
            transform.Rotate(0.0f, 0.5f, 0.0f, Space.Self); 
        }
        else if(gameObject.tag == "CounterClock"){
            transform.Rotate(0.0f, -0.5f, 0.0f, Space.Self); 
        }
    }
}