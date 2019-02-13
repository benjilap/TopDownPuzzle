using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    //CamAnim
    public AnimationCurve transitCurve;

    //PlayerFocus
    GameObject UiPlayer;
    GameObject currentTarget;


    //CamLerp
    bool startMove;
    float startTime;
    float travelTime;

    //CamZoom
    float camZoom = 1;
    [SerializeField]
    float minZoom = 1f;

    [SerializeField]
    float maxZoom = 10f;

    Vector3 initCamPos;

    //CamParam
    [SerializeField]
    float camSpeed;

    [SerializeField]
    float zoomSpeed;


    void Start()
    {
        //UiPlayer = GameObject.FindObjectOfType<Canvas>().gameObject;
        currentTarget = GameObject.FindGameObjectWithTag("Player");

        PlayerFocus(currentTarget.transform.position);
        initCamPos = this.transform.GetChild(0).localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        RotateCam();
        CameraZoom();

        if (Vector3.Distance(transform.position, currentTarget.transform.position) > 0.01f)
        {
            CameraLerp();
        }
        else
        {
            startMove = false;
            PlayerFocus(currentTarget.transform.position);

        }
    }

    public void PlayerFocus(Vector3 pos)
    {

        {
            this.transform.position = pos;
        }
    }

    void RotateCam()
    {

            if (Input.GetKey(KeyCode.Mouse2))
            {

                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + Input.GetAxis("Mouse X") * camSpeed, 0);

            }

    }

    void CameraLerp()
    {
        if (!startMove)
        {
            startTime = Time.time;
            startMove = true;
        }
        travelTime = (Time.time - startTime);


        {
            transform.position = Vector3.Lerp(transform.position, currentTarget.transform.position, transitCurve.Evaluate(travelTime));
        }

    }

    void CameraZoom()
    {
        transform.GetChild(0).localPosition = initCamPos * camZoom;


        if (camZoom < maxZoom)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                camZoom += 0.5f*zoomSpeed;
            }
        }


        if (camZoom > minZoom)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                camZoom -= 0.5f*zoomSpeed;
            }
        }
    }


}
