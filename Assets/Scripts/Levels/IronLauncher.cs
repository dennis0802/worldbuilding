using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronLauncher : MonoBehaviour 
{
    public GameObject missile;

    void Start(){
        Invoke("SpawnMissile", 5.0f);
    }

    void SpawnMissile(){
        GameObject newMissile = Instantiate(missile, transform.position, transform.rotation);
        Invoke("SpawnMissile", 5.0f);
    }
}