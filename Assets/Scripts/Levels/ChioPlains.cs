using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChioPlains : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.plainsTheme.loop = true;
        AudioManager.plainsTheme.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
