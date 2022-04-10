using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 inputVec;
    private InputAction moveAction, jumpAction, runStart, runFinish;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private static MovePlayer instance;
    private Animator animator;
    private int moveXAnimationParameterId, moveZAnimationParameterId, jumpAnimation, floatAnimation, fallAnimation;
    private Vector2 currentAnimationBlendVector;
    private Vector2 animationVelocity;
    private bool groundedPlayer, isRunning;
    [SerializeField]
    private float playerSpeed = 2.0f, jumpHeight = 1.0f, gravityValue = -9.81f, rotationSpeed = 3.0f, 
                  forceMagnitude = 1.0f, animationSmoothTime = 0.1f, animationPlayTransition = 0.15f;
    private int coins = 0, deaths = 0;
    private float timer = 0.0f;
    

    // Based off of: https://www.youtube.com/watch?v=SeBEvM2zMpY
    // Running feature based off of: https://www.youtube.com/watch?v=UqLl53ZPNfo
    private void Start(){
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        runStart = playerInput.actions["RunStart"];
        runFinish = playerInput.actions["RunEnd"];
        runStart.performed += x => PressSprint();
        runFinish.performed += x => ReleaseSprint();
        animator = GetComponentInChildren<Animator>();
        moveXAnimationParameterId = Animator.StringToHash("MoveX");
        moveZAnimationParameterId = Animator.StringToHash("MoveZ");
        //basicAnimation = Animator.StringToHash("Basic Movement");
        jumpAnimation = Animator.StringToHash("Jump");
        floatAnimation = Animator.StringToHash("Float");
        fallAnimation = Animator.StringToHash("Fall");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        // Ground check
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if(isRunning){
            playerSpeed = 4.0f;
        }
        else{
            playerSpeed = 2.0f;
        }

        // Movement dependent on camera's rotation
        Vector2 input = moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);

        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0.0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Animation
        animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
        animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);

        // Jumping
        if(jumpAction.triggered && groundedPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.CrossFade(jumpAnimation, animationPlayTransition);
            AudioManager.jump.Play();
            animator.CrossFade(floatAnimation, animationPlayTransition);
        }

        if(!groundedPlayer && playerVelocity.y < 0){
            animator.CrossFade(fallAnimation, animationPlayTransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Falling out of bounds
        if(transform.position.y < -10){
            AudioManager.fall.Play();
            transform.position = new Vector3(0, 5, 0);
            deaths++;
        }

        // Falling out of boudns (level 3)
        if(transform.position.y < -10 && SceneManager.GetActiveScene().buildIndex == 4){
            AudioManager.fall.Play();
            transform.position = new Vector3(525, 5, 348);
            deaths++;
        }

        timer += Time.deltaTime;
    }
 
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if(rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            AudioManager.coinCollect.Play();
            Debug.Log("Coin collected");
            coins++;
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "End")
        {
            AudioManager.complete.Play();

            // Character controller overrides transform changes, temporarily disable to allow change in position
            // Check if user is approaching level 3 or cleared level 4
            if(SceneManager.GetActiveScene().buildIndex < 3){
                controller.enabled = false;
                gameObject.transform.position = new Vector3(0, 1, 0);
                controller.enabled = true;
            }
            else if(SceneManager.GetActiveScene().buildIndex == 3){
                controller.enabled = false;
                gameObject.transform.position = new Vector3(525, 1, 348);
                controller.enabled = true;
            }
            else if(SceneManager.GetActiveScene().buildIndex == 4){
                // Save stats as player prefs for stat screen to use
                PlayerPrefs.SetInt("Coins", coins);
                PlayerPrefs.SetInt("Deaths", deaths);
                PlayerPrefs.SetFloat("Time", timer);
                Cursor.lockState = CursorLockMode.None;
                Destroy(gameObject);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    void PressSprint(){
        isRunning = true;
    }

    void ReleaseSprint(){
        isRunning = false;
    }
}