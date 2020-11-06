using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arm : MonoBehaviour
{
    [Header("Speed")]
    // SPEED OF ARM MOVING
    public float movementSpeed = 10f;
    public float upDownSpeed = 10f;

    // SPEED OF CLAW ROTATING
    public float rotationSpeed = 45f;

    [Header("Constraints")]
    public float forwardMax = -14.5f;
    public float backwardMax = -12.5f;
    public float leftMax = 14.5f;
    public float rightMax = 22.5f;
    public float vertMax = 3.925f;
    public float vertMin = 2.265f;

    [Header("References")]
    // REFERENCE TO CLAW GAMEOBJECT
    public GameObject claw;

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
    }

    //FUNCTION TO MOVE THE ARM IN 3 DIMENSIONS
    private void Move() // RIGIDBODY MOVE POSITION?
    {
        // MOVE BACKWARD
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z < backwardMax) //HARDCODED VALUE
            {
                transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.z > forwardMax) //HARDCODED VALUE
            {
                transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE LEFT
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x > leftMax) //HARDCODED VALUE
            {
                transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE RIGHT 
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x < rightMax) //HARDCODED VALUE
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE UP
        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y < vertMax) //HARDCODED VALUE
            {
                transform.position += Vector3.up * upDownSpeed * Time.deltaTime;
            }
        }
        // MOVE DOWN
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > vertMin) //HARDCODED VALUE
            {
                transform.position -= Vector3.up * upDownSpeed * Time.deltaTime;
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