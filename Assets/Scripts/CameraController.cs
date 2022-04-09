using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    private Vector3 offset;
    public GameObject player;
    private CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Look for a target
    void LateUpdate()
    {
        if(player == null){
            player = GameObject.FindWithTag("Player");
            if(player != null){
                Target = player.transform;
                vcam.LookAt = Target;
                vcam.Follow = Target;
            }
        }
    }
}
