using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour 
{
    public GameObject missileScanner;
    private GameObject target;
    private Vector3 direction;
    // speed was 20
    private float speed = 1.0f, rotationSpeed = 4.0f, focusDistance = 5.0f;
    private Vector3 lastPos;
    private bool lookingAtTarget = true;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        // Scanning
        target = missileScanner.GetComponent<MissileScan>().target;

        // Movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lastPos = transform.position;

        // Rotation
        if(target != null){
            direction = target.transform.position - transform.position;
            direction.y = 0.01f;
            direction = direction.normalized;

            var rot = Quaternion.LookRotation(direction);

            // When within a certain distance to the player, stop focusing and continue current trajectory
            if(Vector3.Distance(transform.position, target.transform.position) < focusDistance){
                lookingAtTarget = false;
            }
            if(lookingAtTarget){
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider col){
        // For this to work, the environment tagged objects (likely walls, doors, and etc.) need to be kinematic rigidbodies to register
        if(col.CompareTag("Environment")){
            // Play explosion sound
            Debug.Log("Collided with " + col.gameObject);
            Destroy(gameObject);
        }
    }
}