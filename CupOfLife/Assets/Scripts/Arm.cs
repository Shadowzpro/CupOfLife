using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arm : MonoBehaviour
{
    [Header("Speed")]
    // SPEED OF ARM MOVING
    public float leftRightMovementSpeed = 10f;
    public float forwardBackMovementSpeed = 10f;
    public float upDownSpeed = 10f;
    //public float maxVelocityChange = 10f;

    // CURRENT ANGLE OF DRIFT
    public float driftX = 0; // - pushes arm down, + up.
    public float driftY = 0; // - pushes arm Left, + Right.
    public float currentXDrift = 0;
    public float currentYDrift = 0;

    // SPEED OF CLAW ROTATING
    //public float rotationSpeed = 45f;
    //
    //[Header("Constraints")]
    //public float forwardMax = -14.5f;
    //public float backwardMax = -12.5f;
    //public float leftMax = 14.5f;
    //public float rightMax = 22.5f;
    //public float vertMax = 3.925f;
    //public float vertMin = 2.265f;

    [Header("References")]
    // REFERENCE TO CLAW GAMEOBJECT
    public GameObject claw;

    public GameObject cylinder;
    public GameObject cylinderLookAt;

    // Reference to the GameManager
    public GameManager gameManager;
    
    // REFERENCE TO ELBOW OBJECT
    public GameObject elbow;

    // REFERENCE TO ARM'S RIGIDBODY
    private Rigidbody rigidBody;

    // REFERENCE TO LASSSEREZ
    public GameObject laser;

    // Reference to the arm's Animation
    private Animation anim;

    // Reference to the arm's laser sound effect
    private AudioSource laserSFX;
    private bool isAudioPlaying = false;
    private bool isAudioStopping = false;
    private bool isAudioStarting = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animation>();
        laserSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        Debug.Log(laserSFX);

        if (isAudioStopping)
        {
            laserSFX.Stop();
            isAudioStopping = false;
        }
        else if (isAudioStarting)
        {
            isAudioStarting = false;
            laserSFX.Play(0);
        }

        cylinder.transform.LookAt(cylinderLookAt.transform);

        //Move(); // commented out as movement is now in fixed update. this means drift does not work
        //Rotate();
        //UpdateAngleOfDrift();
        if (Input.GetMouseButton(0))
        {
            laser.SetActive(true);
            anim.Play();

            if (!isAudioPlaying)
            {
                isAudioStarting = true;
                isAudioPlaying = true;
            }
        }
        else 
        {
            laser.SetActive(false);
            anim.Rewind();

            if (isAudioPlaying)
            {
                isAudioStopping = true;
                isAudioPlaying = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity.x *= -leftRightMovementSpeed;
        targetVelocity.y *= upDownSpeed;
        targetVelocity.z *= -forwardBackMovementSpeed;

        Vector3 velocity = rigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -leftRightMovementSpeed, leftRightMovementSpeed);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -upDownSpeed, upDownSpeed);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -forwardBackMovementSpeed, forwardBackMovementSpeed);
        rigidBody.AddForce(velocityChange);
    }

    private void UpdateAngleOfDrift() 
    {
        if (driftX > 5) { driftX = 5; }
        if (driftX < -5) { driftX = -5; }
        if (driftY > 5) { driftY = 5; }
        if (driftY < -5) { driftY = -5; }
        
        if (!(Input.GetAxis("Mouse Y") > 0)) //not up
        {
            if (driftX < 0) 
            {
                currentXDrift = -1;
                driftX = driftX + (20 * Time.deltaTime);
            }
        }
        if (!(Input.GetAxis("Mouse X") < 0)) //not left
        {
            if (driftY > 0)
            {
                currentYDrift = 1;
                driftY = driftY - (20 * Time.deltaTime);
            }
        }
        if (!(Input.GetAxis("Mouse Y") < 0)) //not down
        {
            if (driftX > 0)
            {
                currentXDrift = 1;
                driftX = driftX - (20 * Time.deltaTime);
            }
        }
        if (!(Input.GetAxis("Mouse X") > 0)) //not right
        {
            if (driftY < 0)
            {
                currentXDrift = -1;
                driftY = driftY + (20 * Time.deltaTime);
            }
        }

        driftY = Mathf.Clamp(driftY,-5,5);
        driftX = Mathf.Clamp(driftX, -5, 5);

        elbow.transform.localEulerAngles = new Vector3(2*driftX, 2*driftY, 0);
        transform.localEulerAngles = new Vector3(driftX, driftY, 0);




    }
    
    //FUNCTION TO MOVE THE ARM IN 3 DIMENSIONS
    //private void Move()
    //{
    //    // MOVE BACKWARD
    //    //if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    //    //{
    //    //    if (transform.position.z < backwardMax) //HARDCODED VALUE
    //    //    {
    //    //        transform.position += Vector3.forward * forwardBackMovementSpeed * Time.deltaTime;
    //    //    }
    //    //}
    //
    //    // MOVE FORWARD
    //    //if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    //    //{
    //    //    if (transform.position.z > forwardMax) //HARDCODED VALUE
    //    //    {
    //    //        transform.position -= Vector3.forward * forwardBackMovementSpeed * Time.deltaTime;
    //    //    }
    //    //}
    //
    //    // MOVE RIGHT
    //    if (Input.GetAxis("Mouse X") > 0)
    //    {
    //        //if (transform.position.x > leftMax) //HARDCODED VALUE
    //        //{
    //        //    transform.position -= Vector3.right * leftRightMovementSpeed * Time.deltaTime;
    //        //}
    //    
    //        if (driftY > -5) 
    //        {
    //            driftY = (driftY - 10 * Time.deltaTime);
    //        }
    //    
    //    }
    //
    //    // MOVE LEFT 
    //    if (Input.GetAxis("Mouse X") < 0)
    //    {
    //        //if (transform.position.x < rightMax) //HARDCODED VALUE
    //        //{
    //        //    transform.position += Vector3.right * leftRightMovementSpeed * Time.deltaTime;
    //        //}
    //    
    //        if (driftY < 5)
    //        {
    //            driftY = (driftY + 10 * Time.deltaTime);
    //        }
    //    
    //    }
    //
    //    // MOVE UP
    //    if (Input.GetAxis("Mouse Y") > 0)
    //    {
    //        //if (transform.position.y < vertMax) //HARDCODED VALUE
    //        //{
    //        //    transform.position += Vector3.up * upDownSpeed * Time.deltaTime;
    //        //}
    //
    //        if (driftX > -5)
    //        {
    //            driftX = (driftX - 10 * Time.deltaTime);
    //        }
    //
    //    }
    //
    //    // MOVE DOWN
    //    if (Input.GetAxis("Mouse Y") < 0)
    //    {
    //        //if (transform.position.y > vertMin) //HARDCODED VALUE
    //        //{
    //        //    transform.position -= Vector3.up * upDownSpeed * Time.deltaTime;
    //        //}
    //        
    //        if (driftX < 5)
    //        {
    //            driftX = (driftX + 10 * Time.deltaTime);
    //        }
    //
    //    }
    //}

    // FUNCTION TO ROTATE THE CLAW ON 1 AXIS
    //private void Rotate() // RIGIDBODY MOVEROTATION?
    //{
    //    // ROTATE CLOCKWISE
    //    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        claw.transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
    //    }
    //    //ROTATE ANTICLOCKWISE
    //    else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
    //    {
    //        claw.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    //    }
    //}
}