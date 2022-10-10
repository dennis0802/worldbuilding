using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour 
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    void Start(){

    }

    void Update(){
        for(int i = 0; i <= onBelt.Count-1; i++){
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime ;
        }
    }

    // Colliding with the belt
    private void OnCollisionEnter(Collision other){
        onBelt.Add(other.gameObject);
    }

    // Leaving the belt
    private void OnCollisionExit(Collision other) {
        onBelt.Remove(other.gameObject);
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