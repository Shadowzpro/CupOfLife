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

    //public float moveLeftMin = 14;
    //public float moveRightMax = 20;

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
            if (transform.position.z < -12.5)
            {
                transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z > -14.5)
            {
                transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > 14)//hardcode
            {
                transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 20)
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            if (transform.position.y < 3.925)
            {
                transform.position += Vector3.up * movementSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (transform.position.y > 2.265)
            {
                transform.position -= Vector3.up * movementSpeed * Time.deltaTime;
            }
        }

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, moveLeftMin, moveRightMax), transform.position.y, transform.position.z);
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
