using UnityEngine;
using System.Collections;

public class GearPlaced : MonoBehaviour {
    public GameObject gearMachine;

    void Update()
    {
        if(gearMachine.GetComponent<GearSwitch>().on && gameObject.tag == "ClockwiseGear"){
            transform.Rotate(0.0f, 0.5f, 0.0f, Space.Self); 
        }
        else if(gearMachine.GetComponent<GearSwitch>().on && gameObject.tag == "CounterClockGear"){
            transform.Rotate(0.0f, -0.5f, 0.0f, Space.Self); 
        }
    }
}