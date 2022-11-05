using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MovePlayer : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity, respawnLocation = new Vector3(0,5,0);
    private Vector2 inputVec, currentAnimationBlendVector, animationVelocity;
    private InputAction moveAction, jumpAction, runStart, runFinish, waveAction, pickUpAction;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private static MovePlayer instance;
    private Animator animator;
    private float stepCooldown, stepRate = 0.5f;
    private int moveXAnimationParameterId, moveZAnimationParameterId, jumpAnimation, landAnimation, fallAnimation, 
                runAnimation, basicAnimation, waveAnimation, pickUpAnimation;
    private bool groundedPlayer, isRunning, parentChanged, onConveyor;
    [SerializeField]
    private float playerSpeed = 4.0f, jumpHeight = 2.0f, gravityValue = -9.81f, rotationSpeed = 3.0f, 
                  animationSmoothTime = 0.1f, animationPlayTransition = 0.15f, forceMagnitude = 1.0f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public TextMeshProUGUI keyText;
    public static int numKeys;

    // Based off of: https://www.youtube.com/watch?v=SeBEvM2zMpY
    // Running feature based off of: https://www.youtube.com/watch?v=UqLl53ZPNfo
    private void Start(){
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        waveAction = playerInput.actions["Wave"];
        pickUpAction = playerInput.actions["PickUp"];
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
        waveAnimation = Animator.StringToHash("Waving");
        pickUpAnimation = Animator.StringToHash("PickUp");

        numKeys = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){

        // Ground check
        groundedPlayer = controller.isGrounded;
        animator.SetBool("IsGrounded", groundedPlayer);
        stepCooldown -= Time.deltaTime;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        else if(!groundedPlayer && playerVelocity.y < 0){
            animator.CrossFade(fallAnimation, animationPlayTransition-0.12f);
        }

        if(isRunning){
            playerSpeed = 6.0f;
        }
        else{
            playerSpeed = 3.0f;
        }

        // Don't perform certain actions if currently waving/picking up
        if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Waving") || animator.GetCurrentAnimatorStateInfo(0).IsName("PickUp"))){
            // Movement dependent on camera's rotation
            Vector2 input = moveAction.ReadValue<Vector2>();
            currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);

            Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0.0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            if((!Mathf.Approximately(input.x, 0) || !Mathf.Approximately(input.y, 0)) && stepCooldown < 0f && groundedPlayer){
                AudioManager.footstep.pitch = 1.0f + Random.Range(-0.2f, 0.2f);
                AudioManager.footstep.PlayOneShot(AudioManager.footstep.clip, 0.4f);
                if(!isRunning){
                    stepCooldown = stepRate;
                }
                else{
                    stepCooldown = 0.25f;
                }
            }
        }

        // Animation
        animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
        animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);

        // Jumping
        if(jumpAction.triggered && groundedPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.CrossFade(jumpAnimation, animationPlayTransition);
            AudioManager.jump.Play();
            
            if(parentChanged){
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                parentChanged = false;
            }
        }

        // Waving
        if(waveAction.triggered && groundedPlayer){
            animator.CrossFade(waveAnimation, animationPlayTransition);
        }
        // Picking up
        if(pickUpAction.triggered && groundedPlayer){
            animator.CrossFade(pickUpAnimation, animationPlayTransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Falling out of bounds - replaced with death zone triggers as of Oct 22, 2022
        /*
        if(transform.position.y < -10){
            AudioManager.fall.Play();
            transform.position = respawnLocation;
        }
        */

        if(SceneManager.GetActiveScene().buildIndex == 1){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("TransitionTest")){
            controller.enabled = false;
            transform.position = new Vector3(0,0,0);
            AudioManager.complete.Play();
            controller.enabled = true;
        }
        // Going from Chio Plains to Witcher's Tower
        else if(other.gameObject.CompareTag("PlainsToTower")){
            controller.enabled = false;
            transform.position = new Vector3(75,0,30);
            AudioManager.complete.Play();
            AudioManager.plainsTheme.Stop();
            controller.enabled = true;
            SceneManager.LoadScene(5);
        }
        // Going from Witcher's Tower to Chio Plains
        else if(other.gameObject.CompareTag("TowerToPlains")){
            controller.enabled = false;
            transform.position = new Vector3(29,0,-239);
            AudioManager.complete.Play();
            AudioManager.towerTheme.Stop();
            controller.enabled = true;
            SceneManager.LoadScene(3);
        }
        // Going inside the tower from ground floor
        else if(other.gameObject.CompareTag("IntoGroundFloorTower")){
            controller.enabled = false;
            transform.position = new Vector3(-2,1,-20);
            controller.enabled = true;
            SceneManager.LoadScene(6);
        }
        // Going out the tower on ground floor
        else if(other.gameObject.CompareTag("ExitGroundFloor")){
            controller.enabled = false;
            transform.position = new Vector3(73,0,78);
            controller.enabled = true;
            SceneManager.LoadScene(5);
        }
        // Going to tower top
        else if(other.gameObject.CompareTag("EnterTowerTop")){
            controller.enabled = false;
            transform.position = new Vector3(47,122,107);
            controller.enabled = true;
            SceneManager.LoadScene(5);
        }
        // Going back in tower from top
        else if(other.gameObject.CompareTag("ExitTowerTop")){
            controller.enabled = false;
            transform.position = new Vector3(-24, 117.83f, -3);
            controller.enabled = true;
            SceneManager.LoadScene(6);
        }
        // Going from plains to Iron Mountain
        else if(other.gameObject.CompareTag("PlainsToIron")){
            controller.enabled = false;
            transform.position = new Vector3(-140, 2.39f, -402);
            controller.enabled = true;
            AudioManager.complete.Play();
            AudioManager.plainsTheme.Stop();
            SceneManager.LoadScene(7);
        }
        // Going from Iron Mountain to plains
        else if(other.gameObject.CompareTag("IronToPlains")){
            controller.enabled = false;
            transform.position = new Vector3(-35, 0, -415);
            controller.enabled = true;
            AudioManager.complete.Play();
            AudioManager.ironMtTheme.Stop();
            SceneManager.LoadScene(3);
        }        

        // Ground entrance of Iron Mountain's caves
        else if(other.gameObject.CompareTag("IronGroundToCave")){
            controller.enabled = false;
            transform.position = new Vector3(0, 0, 0);
            controller.enabled = true;
            SceneManager.LoadScene(8);
        }

        // Peak entrance to cave
        else if(other.gameObject.CompareTag("IronPeakToCave")){
            controller.enabled = false;
            transform.position = new Vector3(-91, 148, 263);
            controller.enabled = true;
            SceneManager.LoadScene(8);
        }     

        // Ground exit of Iron Mountain's caves
        else if(other.gameObject.CompareTag("CaveToOutside")){
            controller.enabled = false;
            transform.position = new Vector3(-470, 23, -300);
            controller.enabled = true;
            SceneManager.LoadScene(7);
        }        

        // Cave exit to peak
        else if(other.gameObject.CompareTag("CaveToPeak")){
            controller.enabled = false;
            transform.position = new Vector3(-551, 123, -247.5f);
            controller.enabled = true;
            SceneManager.LoadScene(7);
        }     

        // Going into Iron Shrine
        else if(other.gameObject.CompareTag("EnterIron")){
            controller.enabled = false;
            transform.position = new Vector3(-0.5f, 0.5f, 2);
            controller.enabled = true;
            SetKeyCounter();
            SceneManager.LoadScene(9);
        }     

        // Exiting Iron Shrine
        else if(other.gameObject.CompareTag("ExitIron")){
            controller.enabled = false;
            transform.position = new Vector3(-553, 125.5f, -191);
            controller.enabled = true;
            keyText.text = "";
            numKeys = 0;
            SceneManager.LoadScene(7);
        }

        // Exiting Iron Shrine via completion
        else if(other.gameObject.CompareTag("IronComplete")){
            controller.enabled = false;
            transform.position = new Vector3(-553, 125.5f, -191);
            controller.enabled = true;
            AudioManager.complete.Play();
            SceneManager.LoadScene(7);
        }      

        // Contacting a death zone (relevant to shrines/dungeons)
        else if(other.gameObject.CompareTag("DeathZone") || other.gameObject.CompareTag("LaserX") || other.gameObject.CompareTag("LaserY") || 
                other.gameObject.CompareTag("LaserZ")){
            controller.enabled = false;
            AudioManager.fall.Play();
            transform.position = respawnLocation;
            controller.enabled = true;
        }

        // Key collectables
        else if(other.gameObject.CompareTag("Key")){
            AudioManager.complete.Play();
            numKeys++;
            other.gameObject.SetActive(false);
            SetKeyCounter();
        }

        else if(other.gameObject.CompareTag("KeyFragment")){
            AudioManager.complete.Play();
            other.gameObject.SetActive(false);
        }

        else if(other.gameObject.CompareTag("ClockwiseGear")){
            AudioManager.complete.Play();
            other.gameObject.SetActive(false);
        }

        else if(other.gameObject.CompareTag("CounterClockGear")){
            AudioManager.complete.Play();
            other.gameObject.SetActive(false);
        }

        // Reassign respawn point (relevant to dungeons and death zones)
        else if(other.gameObject.CompareTag("Checkpoint")){
            respawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        else if(other.gameObject.CompareTag("Missile")){
            controller.enabled = false;
            AudioManager.fall.Play();
            transform.position = respawnLocation;
            Destroy(other.gameObject);
            controller.enabled = true;
        }
        // Locked doors
        else if(other.gameObject.CompareTag("LockedDoor")){
            bool locked = other.gameObject.GetComponent<LockedDoor>().locked;

            // Check for sufficient keys and the door is locked
            if(numKeys > 0 && locked){
                numKeys--;
                Debug.Log("You unlocked a door! You now have " + numKeys + " keys.");

                // Unlock all associated doors with the doorway
                List<Rigidbody> doors = other.gameObject.GetComponent<LockedDoor>().doors;
                foreach(Rigidbody door in doors){
                    door.constraints = RigidbodyConstraints.None;
                }
                other.gameObject.GetComponent<LockedDoor>().locked = false;
            }
            // If the door is locked but no keys, play a sound to indicate
            else if(locked){
                // Play locked door sound
            }
        }

    }

    void Awake(){
        Time.timeScale = 1.0f;
        // Check for an existing player that has this script attached
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    void SetKeyCounter(){
        keyText.text = "Keys: " + numKeys.ToString();
    }

    void PressSprint(){
        if(animator != null){
            isRunning = true;
            animator.SetBool("IsRunning", isRunning);
        }
    }

    void ReleaseSprint(){
        if(animator != null){
            isRunning = false;
            animator.SetBool("IsRunning", isRunning);
        }
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit){
        // For rotating platforms
        if(hit.gameObject.tag == "RotatingPlatform"){
            transform.parent = hit.transform;
            parentChanged = true;
        }
        else if(parentChanged){
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            parentChanged = false;
        }

        // Conveyor belts
        if(hit.gameObject.tag == "ConveyorPosX"){
            playerVelocity.x = hit.gameObject.GetComponent<ConveyorBelt>().speed/75;
            onConveyor = true;
        }

        else if(hit.gameObject.tag == "ConveyorNegX"){
            playerVelocity.x = hit.gameObject.GetComponent<ConveyorBelt>().speed * -1.0f/75;
            onConveyor = true;
        }

        else if(hit.gameObject.tag == "ConveyorPosZ"){
            playerVelocity.z = hit.gameObject.GetComponent<ConveyorBelt>().speed/75;
            onConveyor = true;
        }

        else if(hit.gameObject.tag == "ConveyorNegZ"){
            playerVelocity.z = hit.gameObject.GetComponent<ConveyorBelt>().speed * -1.0f/75;
            onConveyor = true;
        }
        else if(onConveyor){
            playerVelocity.x = 0.0f;
            playerVelocity.z = 0.0f;
            onConveyor = false;
        }

        Rigidbody r = hit.collider.attachedRigidbody;
        if(r != null){
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            r.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
}