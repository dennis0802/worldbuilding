using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchersTower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.towerTheme.loop = true;
        AudioManager.towerTheme.volume = 0.4f;
        AudioManager.towerTheme.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
