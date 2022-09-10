using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lighting : MonoBehaviour
{
    bool lightSet = true;
    public GameObject globalLight;

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 8 && lightSet){
            lightSet = !lightSet;
            globalLight.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().buildIndex != 8 && !lightSet){
            lightSet = !lightSet;
            globalLight.SetActive(true);
        }
    }
}