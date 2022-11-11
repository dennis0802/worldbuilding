using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSpindle : MonoBehaviour 
{
    [SerializeField]
    public bool active = false;
    public Quaternion startRot;
    public GameObject gearMachine, gear;

    void Start(){
        startRot = transform.rotation;
    }

    void Update()
    {
        if(!PauseMenu.IsPaused){
            if(active && gameObject.tag == "LowerSpindle" && gearMachine.GetComponent<GearSwitch>().on){
                transform.Rotate(0.0f, -0.1f, 0.0f, Space.Self); 
            }
            else if(active && gameObject.tag == "UpperSpindle" && gearMachine.GetComponent<GearSwitch>().on){
                transform.Rotate(0.0f, 0.1f, 0.0f, Space.Self); 
            }
            // Reset back to original rotation
            else{
                transform.rotation = Quaternion.Lerp(transform.rotation, startRot, Time.time * 0.0009f);
            }
        }
    }
}