using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // for our User Interface needs, such as having messages on screen
//  complete the accelerate and deccelerate functions to work while holding the button utilizing analogue triggers
//  should I complete sound effects in here ? For the time being, it is the only way to properly execute them
public class PlayerControllerTest : MonoBehaviour
{
    #region stuff about movement
    public float playerBallspeed = 10;
    public float motionFrc= 10;
    public float force;
    public float accelerateFrc = 10;
    public float deccelerateFrc = 5;
    public float jumpForce = 4;

    private float playerBallMovementX;  //for the playerBall movement along X axis
    private float playerBallMovementY; //for the playerBall movement along Y axis
                                       //private Vector2 playerBallMovement;  //for the playerBall movement
                                       //private bool playerBallJump;
    #endregion

    public AudioSource JumpSound ;  //Will accept the Hierarchy's Game Object with the audio source
    public AudioSource PickUpSound; //Will accept the Hierarchy's Game Object with the audio source
    public AudioSource SFXSource;
    public List<AudioClip> clips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipsByName;
    
    public float heightMarginForError = .01f;
    //public float distanceToGround = 1f;
    public static int playerBallStaminaMAX = 10;
    public int playerBallStaminaRoll = playerBallStaminaMAX;    
    public double playerBallStaminaJump = playerBallStaminaMAX;
    public bool touchingGround;
    //public float RightTriggerValue = 0f; 

    #region stuff about changeing matterials
    private Rigidbody playerBallRigidbody;
    public Renderer playerBallRenderer;
    public Material playerBallMaterial;
    public Collider playerBallCollider;
    #endregion
    #region stuff about our player's body composition
    //public Collider playerBallBoxCollider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region the playerBall-ball's physics attributes
        playerBallRigidbody = GetComponent<Rigidbody>();
        playerBallCollider = GetComponent<Collider>();
        //playerBallMaterial = Resources.Load<Material>("Materials/MaterialRUBBER"); //collider material
        playerBallRenderer = this.GetComponent<Renderer>();
        //playerBallCollider.material = Resources.Load<PhysicMaterial>("PhysicMaterials/Rubber");
        force = motionFrc ;
        #endregion
        //GameObject effects = GameObject.Find("Effects");
        //JumpSound = effects.GetComponents<AudioSource>().Find("Jump");
        //PickUpSound = effects.GetComponents<AudioSource>.Find("PickUp");
        //audioSource = Game
        SFXSource = GetComponent<AudioSource>();
        clipsByName = clips.ToDictionary(c => c.name, c => c); 
        
    }

    void FixedUpdate()
    {
        //  Here we calculate for any physics object, that is, anything with a rigidbody component
        #region Check if our body touches the ground - could be moved to functions that need it only, such as Jump and Spin
        if (Physics.Raycast(transform.position, Vector3.down, playerBallCollider.bounds.extents.y + heightMarginForError) != false) { touchingGround = true; }  //transform.position is equal with playerBallCollider.center
        else touchingGround = false;
        #endregion
        #region Player movement on the 2D plane
        Vector3 playerBallMovement = new Vector3(playerBallMovementX, 0.0f, playerBallMovementY);
        if (playerBallRigidbody.velocity.magnitude < 20) 
        {
            //Debug.Log(playerBallRigidbody.velocity.magnitude);
            playerBallRigidbody.AddForce(Camera.main.transform.TransformDirection(playerBallMovement) * force); //  * Time.deltaTime is used for frame rate independence. it is the proper way to implement a an object's motion  //Camera.main.transform.TransformDirection( playerBallMovement ) is used in order to always have the facing forward directions be the camera's
        }
        
        /* //this is done by the CineMachine Camera extra component
        Vector3 direction = Camera.main.transform.TransformDirection(playerBallMovement.normalized);  // normalized means having values [0, 1]
        gameObject.transform.LookAt(gameObject.transform.position + direction);  //player looking directions
        // rotate the player to the direction of the camera's "forward"
        playerBall.transform.eulerAngles = new Vector3(playerBall.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, playerBall.transform.eulerAngles.z);
        // "translate" the input vector to player coordinates
        inputVector = player.transform.TransformDirection(inputVector);
        playerBallRigidbody.velocity = inputVector * playerBallspeed;*/
        #endregion

        #region Our player's physics condition

        #endregion
        #region Player movement on jump needs test to see if it should be inside Jump function
        /*  if (playerBallJumpstamina <= 0) { playerBallJumpstamina = 0;  playerBallJump = false; }
          if (playerBallJump)
          {            
              playerBallRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
              playerBallJump = false;
          }
          if (playerBallJumpstamina < playerBallJumpstaminaMAX) { playerBallJumpstamina= playerBallJumpstamina + playerBallJumpstamina*0.2 ; }
          if (playerBallJumpstamina > playerBallJumpstaminaMAX) { playerBallJumpstamina = playerBallJumpstaminaMAX; }
         */
        #endregion

        #region add sonic spin charge

        #endregion

        #region add brake

        #endregion

    }

    private void Update()
    {
        //public float triggerInput = Input.GetAxis("RightTrigger") ;
    }
    /*  private bool IsGrounded()
      {
          float heightMarginForError = .01f;
          float distanceToGround = 1f;
          //RaycastHit raycastHit = Physics.Raycast(transform.position, Vector3.down, distanceToGround + heightMarginForError);
          // RaycastHit raycastHit = Physics.Raycast(playerBallBoxCollider.bounds.center, Vector3.down, playerBallBoxCollider.bounds.extents.y + heightMarginForError);
          // debug
          //Color rayColor;
          if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + heightMarginForError) != false) { touchingGround = true; }
          else { touchingGround = false; }
          //Debug.DrawRay(playerBallBoxCollider.bounds.center, Vector3.down * (playerBallBoxCollider.bounds.extents.y + heightMarginForError));
          // debug end
          return Physics.Raycast(transform.position, Vector3.down, distanceToGround + heightMarginForError) ;
      }*/
    #region control actions' inputs and events
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed) //  needed, otherwise the action is called at least twice due to Unity's weird/bugged control system.
        {
            playerBallMovementX = context.ReadValue<Vector2>().x;
            playerBallMovementY = context.ReadValue<Vector2>().y;
            //playerBallMovement = context.ReadValue<Vector2>();
        }
    }

    //  Need to implement the sound effect call here, since the call through the input system package happens 2ice
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started) //  needed, otherwise the action is called at least twice due to Unity's weird/bugged control system.
        {
            if (touchingGround)
            {
                //UnityEngine.Debug.Log(context.control.device.displayName);  //remove after game works flawlessly
                playerBallRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //JumpSound.Play();
                /*AudioClip SoundClip;
                if (clipsByName.TryGetValue("Jump", out SoundClip))
                {
                    SFXSource.clip = SoundClip;
                }
                SFXSource.clip.Pla*/
                //SFXSource.clip = clips.FirstOrDefault(c => c.name == "Jump");
                //SFXSource.clip.Play();
            }
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        /*while (0 < playerBallRollstamina)
        {
            playerBallspeed++;
           playerBallRollstamina--;
        }*/
        //RightTriggerValue = context.ReadValue<float>();  // This is the value it gets from pressing the Accelerate button
        if (context.performed)
        {
            if (touchingGround)
            {                
                force = motionFrc + accelerateFrc * context.ReadValue<float>();
            }
        }
        if (context.canceled) { force = motionFrc; }
    }

    public void Deccelerate(InputAction.CallbackContext context)
    {
        if (context.started)    {/* force = deccelerateFrc;*/ playerBallRigidbody.drag = 3 ; playerBallRigidbody.angularDrag = 3; }
        if (context.canceled) { /*force = motionFrc;*/  playerBallRigidbody.drag = 0 ; playerBallRigidbody.angularDrag = 0.05f; }
    }
    #endregion

    #region Pickups behaviour
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            playerBallRenderer.material = other.GetComponent<Renderer>().material;
            playerBallCollider.material = other.material;
            playerBallRigidbody.mass = other.GetComponent<Rigidbody>().mass;
            PickUpSound.Play(); // sound effect !
        }
    }
    #endregion

    public void ZoomIn(InputAction.CallbackContext context)
    {
        Camera.main.transform.position = Camera.main.transform.position * 2;
    }
}
