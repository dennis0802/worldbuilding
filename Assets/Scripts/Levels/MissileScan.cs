using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Scans for the FIRST player found in radius
public class MissileScan : MonoBehaviour 
{
    public GameObject target = null, missile;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(target == null){
                target = other.gameObject;
            }
        }
    }

}