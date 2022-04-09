using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float countdown = 3.0f;

    // Update is called once per frame
    void Update(){
        // Delay showing the menu for countdown amount of time
        if(countdown > 0){
            countdown -= Time.deltaTime;
        }
        if(countdown < 0){
            SceneManager.LoadScene(1);
        }
    }

    // If impatient and don't want to wait for the countdown to finish,
    // user can click to go to menu
    public void LoadMenu(){
        AudioManager.buttonClick.Play();
        SceneManager.LoadScene(1);
    }
}
