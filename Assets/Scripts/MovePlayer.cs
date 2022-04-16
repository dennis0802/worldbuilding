using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 inputVec, currentAnimationBlendVector, animationVelocity;
    private InputAction moveAction, jumpAction, runStart, runFinish;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private static MovePlayer instance;
    private Animator animator;
    private int moveXAnimationParameterId, moveZAnimationParameterId, jumpAnimation, landAnimation, fallAnimation, 
                runAnimation, basicAnimation;
    private bool groundedPlayer, isRunning;
    [SerializeField]
    private float playerSpeed = 2.0f, jumpHeight = 1.0f, gravityValue = -9.81f, rotationSpeed = 3.0f, 
                  animationSmoothTime = 0.1f, animationPlayTransition = 0.15f;

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
        basicAnimation = Animator.StringToHash("Basic Movement");
        jumpAnimation = Animator.StringToHash("Jump");
        landAnimation = Animator.StringToHash("Land");
        fallAnimation = Animator.StringToHash("Falling");
        runAnimation = Animator.StringToHash("Running");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        // Ground check
        groundedPlayer = controller.isGrounded;
        animator.SetBool("IsGrounded", groundedPlayer);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else if(!groundedPlayer && playerVelocity.y < 0){
            animator.CrossFade(fallAnimation, animationPlayTransition-0.12f);
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
        animator.SetBool("IsRunning", isRunning);
    }

    void ReleaseSprint(){
        isRunning = false;
        animator.SetBool("IsRunning", isRunning);
    }
}