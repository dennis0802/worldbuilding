using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearSwitch : MonoBehaviour
{
    public bool clockwise = false, on = false; 
    public GameObject opposingMachine, gear, lightObj;

    void Start(){
        lightObj.GetComponent<Renderer>().material.color = Color.red;
    }

    void Update(){
        // Power on
        if(on){
            lightObj.GetComponent<Renderer>().material.color = Color.green;
        }
        else{
            lightObj.GetComponent<Renderer>().material.color = Color.red;
        }

        // Automatically turn off if the opposing machine was turned on
        if(opposingMachine.GetComponent<GearSwitch>().on){
            on = false;
            lightObj.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}