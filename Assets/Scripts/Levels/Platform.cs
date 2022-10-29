using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour 
{
    // Allow player to move with the platform
    // Auto Sync Transforms must be used to work with CharacterControllers
    void OnTriggerEnter(Collider other){
        if(other.transform.tag == "Player"){
            other.transform.SetParent(transform, true);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.transform.tag == "Player"){
            other.transform.SetParent(null);
            other.transform.localScale = new Vector3(1,1,1);
            DontDestroyOnLoad(other);
        }
    }
}