using UnityEngine;
using System.Collections;

public class GearPlaced : MonoBehaviour {
    public GameObject gearMachine;

    void Update()
    {
        if(gearMachine.GetComponent<GearSwitch>().on && gameObject.tag == "ClockwiseGear"){
            transform.Rotate(new Vector3(15, 30, 0) * Time.deltaTime);
        }
        else if(gearMachine.GetComponent<GearSwitch>().on && gameObject.tag == "CounterClockGear"){
            transform.Rotate(new Vector3(-15, -30, 0) * Time.deltaTime);
        }
    }
}