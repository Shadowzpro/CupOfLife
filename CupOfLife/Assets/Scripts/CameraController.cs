using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // SMOOTH VALUE
    public float damp = 0.2f; //POSSIBLY NOT NEEDED ANYMORE

    // TRANSFORM OF THE CAMERA
    public Transform cameraTransform;

    //reference to actual camera object
    public GameObject cameraObj;

    // REFERENCE TO PLAYER GAMEOBJECT
    private GameObject player;

    // QUARTERNION OF CAMERA ROTATION
    private Quaternion camRotation; //POSSIBLY NOT NEEDED ANYMORE

    // MAXIMUM ANGLE ON RIGHT SIDE
    public float lookRightMax = 45; //POSSIBLY NOT NEEDED ANYMORE

    // MINIMUM ANGLE ON LEFT SIDE
    public float lookLeftMin = -45; //POSSIBLY NOT NEEDED ANYMORE

    public float lookAtYAxis = 3.25f;
    public float lookAtZAxis = -24.5f;

    public float cameraMinX;
    public float cameraMaxX;



    private void Awake()
    {
        // PLAYER GAMEOBJECT IS INITIALIZED AS OBJECT WITH TAG "DESTINATION"
        player = GameObject.FindGameObjectWithTag("Destination");

        cameraMinX = 15.5f;
        cameraMaxX = 21.5f;
    }

    private void Update()
    {
        
        
        if (cameraTransform != null)
        {

            Vector3 startPos = cameraTransform.position;
            Vector3 endPos = player.transform.position;
            if (endPos.x < cameraMinX) { endPos.x = cameraMinX; }
            if (endPos.x > cameraMaxX) { endPos.x = cameraMaxX; }
            endPos.y = startPos.y;
            endPos.z = startPos.z;

            cameraTransform.position = Vector3.Lerp(startPos, endPos, 10* Time.deltaTime);

            // CAMERA LOOKS AT THE PLAYER'S X-POSITION AS IT MOVES LEFT AND RIGHT 
            cameraTransform.LookAt(new Vector3(player.transform.position.x, lookAtYAxis, lookAtZAxis));

            
                
        }
    }
}
