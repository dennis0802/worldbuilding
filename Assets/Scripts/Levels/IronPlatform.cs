using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronPlatform : MonoBehaviour 
{
    public float speed = 5.0f;
    public bool goPos = true;
    Vector3 max, min;

    void Start()
    {
        if(gameObject.tag == "PlatformX"){
            max = new Vector3(transform.position.x+6.0f, transform.position.y, transform.position.z);
            min = new Vector3(transform.position.x-6.0f, transform.position.y, transform.position.z);
        }
        else if(gameObject.tag == "PlatformZ"){
            max = new Vector3(transform.position.x, transform.position.y, transform.position.z+6.0f);
            min = new Vector3(transform.position.x, transform.position.y, transform.position.z-6.0f);
        }
    }

    void Update()
    {
        var step = Time.deltaTime * speed;
        if(gameObject.tag == "PlatformX"){
            if(transform.position.x >= max.x){
                goPos = false;
            }
            else if(transform.position.x <= min.x){
                goPos = true;
            }

            if(goPos){
                transform.position = Vector3.MoveTowards(transform.position, max, step);
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position, min, step);
            }
        }

        else if(gameObject.tag == "PlatformZ"){
            if(transform.position.z >= max.z){
                goPos = false;
            }
            else if(transform.position.z <= min.z){
                goPos = true;
            }

            if(goPos){
                transform.position = Vector3.MoveTowards(transform.position, max, step);
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position, min, step);
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