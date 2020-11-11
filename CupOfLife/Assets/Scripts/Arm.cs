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

    //CURRENT ANGLE OF DRIFT
    public float driftX = 0; // - pushes arm down, + up.
    public float driftY = 0; // - pushes arm Left, + Right.
    public float currentXDrift = 0;
    public float currentYDrift = 0;

    // SPEED OF CLAW ROTATING
    public float rotationSpeed = 45f;

    [Header("Constraints")]
    public float forwardMax = -14.5f;
    public float backwardMax = -12.5f;
    public float leftMax = 14.5f;
    public float rightMax = 22.5f;
    public float vertMax = 3.925f;
    public float vertMin = 2.265f;

    //private Vector3 xMouseMovement;
    //private Vector3 yMouseMovement;
    //private Vector3 zMouseMovement;

    [Header("References")]
    // REFERENCE TO CLAW GAMEOBJECT
    public GameObject claw;
    
    // REFERENCE TO ELBOW OBJECT
    public GameObject elbow;
    //Reference to other joint?

    // REFERENCE TO ARM'S RIGIDBODY
    private Rigidbody rigidBody;

    //REFERENCE TO LASSSEREZ
    public GameObject laser;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //xMouseMovement = HorizontalMovement();
        //yMouseMovement = VerticalMovement();
        //zMouseMovement = ForwardMovement();
        Move(); // commented out so that horizontal and vertical movement work. this means drift does not work
        Rotate();
        UpdateAngleOfDrift();
        if (Input.GetMouseButton(0))
        {
            laser.SetActive(true);
        }
        else 
        {
            laser.SetActive(false);
        }

    }

    //private void FixedUpdate() //not smooth
    //{
    //    Vector3 new_position = transform.position + xMouseMovement + yMouseMovement;// + zMouseMovement;
    //    new_position.x = Mathf.Clamp(new_position.x, leftMax, rightMax);
    //    new_position.y = Mathf.Clamp(new_position.y, vertMin, vertMax);
    //    //new_position.z = Mathf.Clamp(new_position.z, backwardMax, forwardMax);
    //    rigidBody.MovePosition(new_position);
    //}
    public Vector3 HorizontalMovement()
    {
        Vector3 xMouseMovement = new Vector3(Input.GetAxisRaw("Mouse X"), 0, 0);

        return xMouseMovement * -leftRightMovementSpeed * Time.fixedDeltaTime;//deltatime?
    }

    public Vector3 VerticalMovement()
    {
        Vector3 yMouseMovement = new Vector3(0, Input.GetAxisRaw("Mouse Y"), 0);

        return yMouseMovement * upDownSpeed * Time.fixedDeltaTime;//delta time?
    }

    public Vector3 ForwardMovement() // currently bypassing set bounds
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        scroll = Mathf.Clamp(scroll, scroll - backwardMax, scroll + forwardMax);//probably not right
        Vector3 zMouseMovement = new Vector3(0, 0, scroll);

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (rigidBody.position.z < backwardMax)
            {
                return zMouseMovement * forwardBackMovementSpeed * Time.fixedDeltaTime;//delta time?

            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (rigidBody.position.z > forwardMax)
            {
                return zMouseMovement * -forwardBackMovementSpeed * Time.fixedDeltaTime;//delta time?
            }
        }
        return zMouseMovement *=0;
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
    private void Move()
    {
        // MOVE BACKWARD
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (transform.position.z < backwardMax) //HARDCODED VALUE
            {
                transform.position += Vector3.forward * forwardBackMovementSpeed * Time.deltaTime;
            }
        }

        // MOVE FORWARD
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (transform.position.z > forwardMax) //HARDCODED VALUE
            {
                transform.position -= Vector3.forward * forwardBackMovementSpeed * Time.deltaTime;
            }
        }

        // MOVE RIGHT
        if (Input.GetAxis("Mouse X") > 0)
        {
            if (transform.position.x > leftMax) //HARDCODED VALUE
            {
                transform.position -= Vector3.right * leftRightMovementSpeed * Time.deltaTime;
            }
        
            if (driftY > -5) 
            {
                driftY = (driftY - 10 * Time.deltaTime);
            }
        
        }

        // MOVE LEFT 
        if (Input.GetAxis("Mouse X") < 0)
        {
            if (transform.position.x < rightMax) //HARDCODED VALUE
            {
                transform.position += Vector3.right * leftRightMovementSpeed * Time.deltaTime;
                //inputVector = new Vector3(leftRightMovementSpeed, 0, 0);
                //rigidBody.velocity = inputVector;
            }
        
            if (driftY < 5)
            {
                driftY = (driftY + 10 * Time.deltaTime);
            }
        
        }

        // MOVE UP
        if (Input.GetAxis("Mouse Y") > 0)// || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y < vertMax) //HARDCODED VALUE
            {
                transform.position += Vector3.up * upDownSpeed * Time.deltaTime;
            }

            if (driftX > -5)
            {
                driftX = (driftX - 10 * Time.deltaTime);
            }

        }

        // MOVE DOWN
        if (Input.GetAxis("Mouse Y") < 0)// || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > vertMin) //HARDCODED VALUE
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