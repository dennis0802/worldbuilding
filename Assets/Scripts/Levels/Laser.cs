using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour 
{
    public float speed = 5.0f;
    public bool goPos = true;
    Vector3 max, min;

    void Start()
    {
        if(gameObject.tag == "LaserX"){
            max = new Vector3(transform.position.x+6.0f, transform.position.y, transform.position.z);
            min = new Vector3(transform.position.x-6.0f, transform.position.y, transform.position.z);
        }
        else if(gameObject.tag == "LaserY"){
            max = new Vector3(transform.position.x, transform.position.y+6.0f, transform.position.z);
            min = new Vector3(transform.position.x, transform.position.y-6.0f, transform.position.z);
        }
        else if(gameObject.tag == "LaserZ"){
            max = new Vector3(transform.position.x, transform.position.y, transform.position.z+6.0f);
            min = new Vector3(transform.position.x, transform.position.y, transform.position.z-6.0f);
        }
    }

    void Update()
    {
        var step = Time.deltaTime * speed;
        if(gameObject.tag == "LaserX"){
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

        else if(gameObject.tag == "LaserY"){
            if(transform.position.y >= max.y){
                goPos = false;
            }
            else if(transform.position.y <= min.y){
                goPos = true;
            }

            if(goPos){
                transform.position = Vector3.MoveTowards(transform.position, max, step);
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position, min, step);
            }
        }
        
        else if(gameObject.tag == "LaserZ"){
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
}