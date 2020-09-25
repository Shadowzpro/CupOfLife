using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //SPEED OF ARM MOVING
    public float movementSpeed = 10f;
    //SPEED OF CLAW ROTATING
    public float rotationSpeed = 45f;

    //REFERENCE TO CLAW GAMEOBJECT
    public GameObject claw;

    //REFERENCE TO ARM'S RIGIDBODY
    private Rigidbody rigidBody;

    //
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    //WHEN ARM IS ENABLED IT CAN MOVE
    private void OnEnable()
    {
        rigidBody.isKinematic = false;
    }

    //WHEN ARM IS DISABLED IT CAN NOT MOVE
    private void OnDisable()
    {
        rigidBody.isKinematic = false;
    }

    //
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    //FUNCTION TO MOVE THE ARM IN 3 DIMENSIONS
    private void Move()
    {
        //NEED TO SET BOUNDS
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.position -= Vector3.up * movementSpeed * Time.deltaTime;
        }
    }

    //FUNCTION TO ROTATE THE CLAW IN 1 DIMENSION
    private void Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            claw.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            claw.transform.Rotate(-Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}
