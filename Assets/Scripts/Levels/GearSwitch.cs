using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearSwitch : MonoBehaviour
{
    public bool clockwise = false, on = false; 
    public GameObject opposingMachine, gear;

    void Update(){
        // Automatically turn off if the opposing machine was turned on
        if(opposingMachine.GetComponent<GearSwitch>().on){
            on = !on;
        }
    }
}