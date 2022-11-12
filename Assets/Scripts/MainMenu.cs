using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    // Based off of: https://www.youtube.com/watch?v=zc8ac_qUXQY
    // Each menu option
    // NOTE: According to build index settings on the project
    // 0 = splash screen
    // 1 = menu
    // 2 = splash screen
    // 3 = start off at the Spirit Maple (currently Fettuccine Plains)
    // 4 = debug
    // 9 = current development focus (Iron Shrine)

    public void Play(){
        AudioManager.buttonClick.Play();
        SceneManager.LoadScene(4);
        Debug.Log("Projected finished date is Dec 31, 2024");
    }

    public void Controls(){
        AudioManager.buttonClick.Play();
        SceneManager.LoadScene(2);
    }

    public void ReturnToMenu(){
        AudioManager.buttonClick.Play();
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        AudioManager.buttonClick.Play();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            UnityEngine.Application.Quit();
        #endif
    }
}
