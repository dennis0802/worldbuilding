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
        max = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        min = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
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
}
