using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour 
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        var speedStep = 5.0f * Time.deltaTime;
        var rotStep = 10.0f * Time.deltaTime;
        Vector3 targetDir = player.transform.position - transform.position;
        targetDir = targetDir.normalized;
        var rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotStep);
        transform.Translate(Vector3.forward * speedStep);
    }
}