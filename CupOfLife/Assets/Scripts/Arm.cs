using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arm : MonoBehaviour
{
    [Header("Speed")]
    // SPEED OF ARM MOVING
    public float movementSpeed = 10f;
    public float upDownSpeed = 10f;

    //CURRENT ANGLE OF DRIFT
    public float driftX = 0; // - pushes arm down, + up.
    public float driftY = 0; // - pushes arm Left, + Right.
    public float currentXDrift = 0;
    public float currentYDrift = 0;

    // SPEED OF CLAW ROTATING
    public float rotationSpeed = 45f;

    [Header("Constraints")]
    public float forward = -14.5f;
    public float backward = -12.5f;
    public float left = 22.5f;
    public float right = 14.5f;
    public float up = 3.925f;

    [Header("References")]
    // REFERENCE TO CLAW GAMEOBJECT
    public GameObject claw;
    
    // REFERENCE TO ELBOW OBJECT
    public GameObject elbow;
    //Reference to other joint?

    // REFERENCE TO ARM'S RIGIDBODY
    private Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() // SHOULD I CHANGE THIS TO UPDATE?
    {
        Move();
        Rotate();
        UpdateAngleOfDrift();
    }

    private void UpdateAngleOfDrift() 
    {
        if (driftX > 5) { driftX = 5; }
        if (driftX < -5) { driftX = -5; }
        if (driftY > 5) { driftY = 5; }
        if (driftY < -5) { driftY = -5; }
        
        if (!Input.GetKey(KeyCode.R)) 
        {
            if (driftX < 0) 
            {
                currentXDrift = -1;
                driftX = driftX + (20 * Time.deltaTime);
            }
        }
        if (!Input.GetKey(KeyCode.A))
        {
            if (driftY > 0)
            {
                currentYDrift = 1;
                driftY = driftY - (20 * Time.deltaTime);
            }
        }
        if (!Input.GetKey(KeyCode.F))
        {
            if (driftX > 0)
            {
                currentXDrift = 1;
                driftX = driftX - (20 * Time.deltaTime);
            }
        }
        if (!Input.GetKey(KeyCode.D))
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
    private void Move() // RIGIDBODY MOVE POSITION?
    {
        // MOVE BACKWARD
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z < backward) //HARDCODED VALUE
            {
                transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.z > forward) //HARDCODED VALUE
            {
                transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE RIGHT
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x > right) //HARDCODED VALUE
            {
                transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
            }

            if (driftY > -5) 
            {
                driftY = (driftY - 10 * Time.deltaTime);
            }

        }
        // MOVE LEFT 
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x < left) //HARDCODED VALUE
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            }

            if (driftY < 5)
            {
                driftY = (driftY + 10 * Time.deltaTime);
            }

        }
        // MOVE UP
        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y < up) //HARDCODED VALUE
            {
                transform.position += Vector3.up * upDownSpeed * Time.deltaTime;
            }

            if (driftX > -5)
            {
                driftX = (driftX - 10 * Time.deltaTime);
            }

        }
        // MOVE DOWN
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > 2.265) //HARDCODED VALUE
            {
                transform.position -= Vector3.up * upDownSpeed * Time.deltaTime;
            }
            
            if (driftX < 5)
            {
                driftX = (driftX + 10 * Time.deltaTime);
            }

        }
    }

    // FUNCTION TO ROTATE THE CLAW ON 1 AXIS
    private void Rotate() // RIGIDBODY MOVEROTATION?
    {
        // ROTATE CLOCKWISE
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            claw.transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        //ROTATE ANTICLOCKWISE
        else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
        {
            claw.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}