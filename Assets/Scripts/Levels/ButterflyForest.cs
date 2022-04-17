using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyForest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.forestBgm.loop = true;
        AudioManager.forestBgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
