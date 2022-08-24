using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public bool rising = true;
    Vector3 max, min;

    // Start is called before the first frame update
    void Start()
    {
        max = new Vector3(transform.position.x, transform.position.y+1.0f, transform.position.z);
        min = new Vector3(transform.position.x, transform.position.y-1.0f, transform.position.z);
        if(transform.tag == "MainElevator"){
            max = new Vector3(transform.position.x, transform.position.y+17.0f, transform.position.z);
            min = new Vector3(transform.position.x, transform.position.y-9.0f, transform.position.z);
            speed = 3.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var step = Time.deltaTime * speed;
        if(transform.position.y >= max.y){
            rising = false;
        }
        else if(transform.position.y <= min.y){
            rising = true;
        }

        if(rising){
            transform.position = Vector3.MoveTowards(transform.position, max, step);
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, min, step);
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
