using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    private InputAction pauseAction;
    public PlayerInput playerInput;
    private static PauseMenu instance;
    private AudioManager audioManager;
    private float timer = 0.0f;
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    private int priorityBoost = 10;

    // Start is called before the first frame update
    void Start()
    {
        pauseAction = playerInput.actions["Pause"];
        vcam = GameObject.FindWithTag("PauseCamera").GetComponent<CinemachineVirtualCamera>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
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

/*
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
*/

    public void Resume(){
        AudioManager.buttonClick.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
        vcam.Priority -= priorityBoost; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        IsPaused = true;
        vcam.Priority += priorityBoost; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu(){
        IsPaused = false;
        vcam.Priority -= priorityBoost; 
        pauseMenuUI.SetActive(false);
        AudioManager.buttonClick.Play();
        SceneManager.LoadScene(1);
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
