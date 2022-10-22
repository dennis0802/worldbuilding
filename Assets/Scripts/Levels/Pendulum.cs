using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour 
{
    public float MaxAngleDeflection = 50.0f, speed = 20.0f;

    void Update(){
        if(!PauseMenu.IsPaused){
            float angle = MaxAngleDeflection * Mathf.Sin(Time.time * speed);
            if(gameObject.tag == "AxeZ"){
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else if(gameObject.tag == "AxeX"){
                transform.rotation = Quaternion.Euler(angle, 0, 0);
            }
        }
    }
}