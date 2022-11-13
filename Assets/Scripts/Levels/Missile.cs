using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour 
{
    public GameObject missileScanner;
    private GameObject target = null;
    private Vector3 direction;
    private float speed = 10f, rotationSpeed = 4.0f, focusDistance = 4.0f;
    private bool lookingAtTarget = true;

    void Update()
    {
        // Scanning
        target = missileScanner.GetComponent<MissileScan>().target;

        // Movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

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

    /* Solution retired, had resulted in early triggering of trigger
    void OnTriggerEnter(Collider col){
        // For this to work, the environment tagged objects (likely walls, doors, and etc.) need to be kinematic rigidbodies to register
        if(col.CompareTag("Environment")){
            AudioManager.explosion.Play();
            Debug.Log(gameObject + " collided with " + col.gameObject);
            Destroy(gameObject);
        }
    }
    */

    // When colliding with anything, explode
    void OnCollisionEnter(Collision other) {
        AudioManager.explosion.Play();
        Destroy(gameObject);
    }
}