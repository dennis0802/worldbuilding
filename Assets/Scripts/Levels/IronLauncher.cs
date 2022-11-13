using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronLauncher : MonoBehaviour 
{
    public GameObject missile;
    public Quaternion rocketRotation;
    public Vector3 offset;

    void Start(){
        Invoke("SpawnMissile", 5.0f);
    }

    void SpawnMissile(){
        GameObject newMissile = Instantiate(missile, transform.position + offset, rocketRotation);
        Invoke("SpawnMissile", 5.0f);
    }
}