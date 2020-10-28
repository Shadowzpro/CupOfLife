using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // SMOOTH VALUE
    public float damp = 0.2f; //POSSIBLY NOT NEEDED ANYMORE

    // TRANSFORM OF THE CAMERA
    public Transform cameraTransform;

    // REFERENCE TO PLAYER GAMEOBJECT
    private GameObject player;

    // QUARTERNION OF CAMERA ROTATION
    private Quaternion camRotation; //POSSIBLY NOT NEEDED ANYMORE

    // MAXIMUM ANGLE ON RIGHT SIDE
    public float lookRightMax = 45; //POSSIBLY NOT NEEDED ANYMORE

    // MINIMUM ANGLE ON LEFT SIDE
    public float lookLeftMin = -45; //POSSIBLY NOT NEEDED ANYMORE

    private void Awake()
    {
        // PLAYER GAMEOBJECT IS INITIALIZED AS OBJECT WITH TAG "DESTINATION"
        player = GameObject.FindGameObjectWithTag("Destination");
    }

    private void Update()
    {
        if (cameraTransform != null)
        {
            // CAMERA LOOKS AT THE PLAYER'S X-POSITION AS IT MOVES LEFT AND RIGHT 
            cameraTransform.LookAt(new Vector3(player.transform.position.x, -3.5f, 15.4f));
        }
    }
}
