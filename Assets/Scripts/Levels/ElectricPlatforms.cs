using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlatforms : MonoBehaviour 
{
    public GameObject[] platforms;
    public float waitTime = 2.0f, timer = 0.0f;
    private int marker = 0;

    void Update(){
        timer += Time.deltaTime;
        // After 2 seconds, prepare to alternate platforms
        if(timer > waitTime){
            timer = timer - waitTime;
            Time.timeScale = 1.0f;

            // General
            if(marker < platforms.Length/2){
                platforms[marker].SetActive(false);
                platforms[marker+2].SetActive(true);
                marker++;
            }
            // Middle of the list
            else if(marker >= platforms.Length/2 && marker < platforms.Length-1){
                platforms[marker].SetActive(false);
                platforms[marker - platforms.Length/2].SetActive(true);
                marker++;
            }

            // End of the list, 0 is guaranteed to be active so hard set element 1
            else if(marker == platforms.Length-1){
                platforms[marker].SetActive(false);
                platforms[1].SetActive(true);
                marker = 0;
            }
        }
    }
}