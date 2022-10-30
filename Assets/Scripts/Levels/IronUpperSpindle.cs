using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronUpperSpindle : MonoBehaviour 
{
    [SerializeField]
    public bool active = false;
    public Quaternion startRot;

    void Start(){
        startRot = transform.rotation;
    }

    void Update()
    {
        if(!PauseMenu.IsPaused){
            if(active){
                transform.Rotate(0.0f, 0.1f, 0.0f, Space.Self); 
            }
            // Reset back to original rotation
            else{
                transform.rotation = Quaternion.Lerp(transform.rotation, startRot, Time.time * 0.0009f);
            }
        }

    }
}