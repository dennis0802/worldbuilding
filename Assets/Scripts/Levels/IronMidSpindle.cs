using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMidSpindle : MonoBehaviour 
{
    public GameObject target;
    public Quaternion originalRotation;

    void Start(){
        originalRotation = transform.rotation;
    }
    /*
    void Update()
    {
        // Turn counterclock-wise
        if(IronLowerSpindle.clockwiseSwitch){
            transform.RotateAround(target.transform.position, Vector3.up, -20 * Time.deltaTime);  
        }
        // Rotate back to original position
        else if(IronLowerSpindle.clockwiseSwitch){
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.time * 1.0f);
        } 
    }
    */
}