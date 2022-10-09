using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronLowerSpindle : MonoBehaviour 
{
    public static bool active = true;

    void Update()
    {
        if(active){
            transform.Rotate(0.0f, -0.1f, 0.0f, Space.Self); 
        }
        // Reset back to original rotation
        else{
            
        }
    }
}