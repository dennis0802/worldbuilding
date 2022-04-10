using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject levelManager;
    private InputAction pauseAction;
    private PlayerInput playerInput;
    private static PauseMenu instance;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
        PlayerPrefs.SetFloat("Time", timer);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(pauseAction.triggered){
            AudioManager.buttonClick.Play();
            if(IsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    void Awake(){
        // Check for an existing player that has this script attached
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    public void Resume(){
        AudioManager.buttonClick.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        IsPaused = true;
    }

    public void LoadMenu(){
        AudioManager.buttonClick.Play();
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        AudioManager.buttonClick.Play();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            UnityEngine.Application.Quit();
        #endif
    }
}
