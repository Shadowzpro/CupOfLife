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

    //private void Start()
    //{
    //    camRotation = transform.localRotation;
    //}
    private void Awake()
    {
        // PLAYER GAMEOBJECT IS INITIALIZED AS OBJECT WITH TAG "DESTINATION"
        player = GameObject.FindGameObjectWithTag("Destination");
    }

    private void Update()
    {
        //Vector3 targetPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        //this.transform.LookAt(targetPosition);

        //var lookPos = player.transform.position - transform.position;
        //lookPos.x = 13.5f;
        //lookPos.y = 0;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.2f);

        if (cameraTransform != null)
        {
            //cameraTransform.LookAt(new Vector3(player.transform.position.x, transform.position.y, xAngleLook.transform.position.z));

            // CAMERA LOOKS AT THE PLAYER'S X-POSITION AS IT MOVES LEFT AND RIGHT 
            cameraTransform.LookAt(new Vector3(player.transform.position.x, -3.5f, 15.4f));
            
            //camRotation.x = 13.5f;
        }

        //camRotation.y += Input.GetAxis("Horizontal") * damp;
        //
        //camRotation.y = Mathf.Clamp(camRotation.y, lookLeftMin, lookRightMax);
        //
        //transform.localRotation = Quaternion.Euler(13.5f, camRotation.y, camRotation.z);
    }
}
