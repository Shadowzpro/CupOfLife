using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arm : MonoBehaviour
{
    /// SPEED OF ARM MOVING
    public float movementSpeed = 10f;

    /// SPEED OF CLAW ROTATING
    public float rotationSpeed = 45f;

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
        // MOVE FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.z < -12.5) //HARDCODED VALUE
            {
                transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE BACKWARD
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z > -14.5) //HARDCODED VALUE
            {
                transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE LEFT
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > 14) //HARDCODED VALUE
            {
                transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE RIGHT 
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 20) //HARDCODED VALUE
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE UP
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (transform.position.y < 3.925) //HARDCODED VALUE
            {
                transform.position += Vector3.up * movementSpeed * Time.deltaTime;
            }
        }
        // MOVE DOWN
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (transform.position.y > 2.265) //HARDCODED VALUE
            {
                transform.position -= Vector3.up * movementSpeed * Time.deltaTime;
            }
        }
    }

    // FUNCTION TO ROTATE THE CLAW ON 1 AXIS
    private void Rotate() // RIGIDBODY MOVEROTATION?
    {
        // ROTATE CLOCKWISE
        if (Input.GetKey(KeyCode.Q))
        {
            claw.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        //ROTATE ANTICLOCKWISE
        else if (Input.GetKey(KeyCode.E))
        {
            claw.transform.Rotate(-Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}
