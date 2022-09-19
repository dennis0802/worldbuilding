using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWallRotate : MonoBehaviour{
    void Update(){
        if(!PauseMenu.IsPaused){
            transform.Rotate(new Vector3(0,50,0) * Time.deltaTime);
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