using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    //CamAnim

    //PlayerFocus
    GameObject UiPlayer;
    GameObject currentTarget;

    //CamZoom
    float camZoom = 1;
    [SerializeField]
    float minZoom = 1f;
    [SerializeField]
    float maxZoom = 10f;

    //CamParam
    float cameraSmooth = 0.125f;
    Vector3 initCamPos;


    [SerializeField]
    float verticalAngleOffset = 15;
    float minVerticalAngle;
    float maxVerticalAngle;

    //CamParam
    [SerializeField]
    float camSpeedX=1;
    float camSpeedY = 1;
        [SerializeField]
    float zoomSpeed=1;
    [SerializeField]
    float zoomOffset=0.1f;



    void Start()
    {
        //UiPlayer = GameObject.FindObjectOfType<Canvas>().gameObject;
        currentTarget = GameObject.FindGameObjectWithTag("Player");
        initCamPos = this.transform.GetChild(0).localPosition;
        minVerticalAngle = this.transform.eulerAngles.x - verticalAngleOffset;
        maxVerticalAngle = this.transform.eulerAngles.x + verticalAngleOffset;
        camSpeedY = camSpeedX * 0.5f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckTarget();
        RotateCam();
        CameraZoom();
        CameraLerp();

    }

    void CheckTarget()
    {
        if (currentTarget == null)
        {
            currentTarget = GameObject.FindGameObjectWithTag("Player");

        }
    }

    void RotateCam()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            float camXInput = transform.eulerAngles.x + (Input.GetAxis("Mouse Y") * camSpeedY);
            float camXRot = Mathf.Clamp(camXInput, minVerticalAngle, maxVerticalAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(camXRot, transform.eulerAngles.y + (Input.GetAxis("Mouse X") * camSpeedX), 0), cameraSmooth);
        }
        else if (Input.GetAxis("ActiveZoom") == 0)
        {
            float camXInput = transform.eulerAngles.x + (Input.GetAxis("RightStickY") * camSpeedY);
            float camXRot = Mathf.Clamp(camXInput, minVerticalAngle, maxVerticalAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(camXRot, transform.eulerAngles.y + (Input.GetAxis("RightStickX") * camSpeedX), 0), cameraSmooth);
        }
    }

    void CameraLerp()
    {
        if (currentTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, currentTarget.transform.position, cameraSmooth);
        }
    }

    void CameraZoom()
    {
        if (Input.GetAxis("ActiveZoom") != 0)
        {
            if (Input.GetAxis("RightStickY") < 0)
            {
                camZoom += zoomOffset * zoomSpeed;
            }
            else if (Input.GetAxis("RightStickY") > 0)
            {
                camZoom -= zoomOffset * zoomSpeed;
            }
        }
        else
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camZoom -= zoomOffset * zoomSpeed;
        }
        else
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camZoom += zoomOffset * zoomSpeed;
        }
        transform.GetChild(0).localPosition = initCamPos * Mathf.Clamp(camZoom, minZoom, maxZoom);
        
        
    }


}
