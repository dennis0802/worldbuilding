using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour 
{
    private GameObject target;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        var speedStep = 10.0f * Time.deltaTime;
        var rotStep = 40.0f * Time.deltaTime;

        // Move forward
        transform.Translate(Vector3.forward * speedStep);

        // Target if exists
        if(target != null){
            Vector3 targetDir = target.transform.position - transform.position;
            targetDir.y = -0.1f;
            targetDir = targetDir.normalized;
            var rot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotStep);
        }
    }

    void OnTriggerEnter(Collider col){
        Debug.Log("collision detected");
        if(col.CompareTag("Environment")){
            Debug.Log("Collided with environment");
            Destroy(gameObject);
        }
    }
}