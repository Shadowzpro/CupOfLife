using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float damp = 0.2f;
    public Transform cameraTransform;
    private GameObject player;
    public GameObject xAngleLook;

    private Quaternion camRotation;
    public float lookRightMax = 45;
    public float lookLeftMin = -45;

    private void Start()
    {
        camRotation = transform.localRotation;
    }
    private void Awake()
    {
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
