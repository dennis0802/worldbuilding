using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMountain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.ironMtTheme.loop = true;
        AudioManager.ironMtTheme.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
