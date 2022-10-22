using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour 
{
    void Update()
    {
        if(!PauseMenu.IsPaused){
            if(gameObject.tag == "Clockwise"){
                transform.Rotate(0.0f, 0.5f, 0.0f, Space.Self); 
            }
            else if(gameObject.tag == "CounterClock"){
                transform.Rotate(0.0f, -0.5f, 0.0f, Space.Self); 
            }
        }
    }

    // Allow player to move with the platform
    // Auto Sync Transforms must be used to work with CharacterControllers
    void OnTriggerEnter(Collider other){
        if(other.transform.tag == "Player"){
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.transform.tag == "Player"){
            other.transform.parent = null;
            DontDestroyOnLoad(other);
        }
    }
}