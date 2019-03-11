using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float playerSpeed=1;

    Vector3 xAxis;
    Vector3 yAxis;

    GameObject myPivotCamera;


	void Start () {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
	}
	
	void Update () {
        UpdatePlayerAxis();
        PlayerMovement();

	}

    void UpdatePlayerAxis()
    {

        xAxis = new Vector3((Mathf.Cos(myPivotCamera.transform.eulerAngles.y * Mathf.Deg2Rad)), 0, (-Mathf.Sin(myPivotCamera.transform.eulerAngles.y * Mathf.Deg2Rad)));
        yAxis = new Vector3((-Mathf.Cos((myPivotCamera.transform.eulerAngles.y+90) * Mathf.Deg2Rad )), 0, (Mathf.Sin((myPivotCamera.transform.eulerAngles.y+90) * Mathf.Deg2Rad )));

        Debug.DrawLine(transform.position, transform.position+xAxis, Color.red);
        Debug.DrawLine(transform.position, transform.position+yAxis, Color.blue);
    }

    void PlayerMovement()
    {
        Vector3 gravity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);
        if(Input.GetAxis("Horizontal")!=0|| Input.GetAxis("Vertical") != 0)
        {
            this.GetComponent<Rigidbody>().velocity = gravity+((xAxis * Input.GetAxis("Horizontal"))+ (yAxis * Input.GetAxis("Vertical")))*playerSpeed;

        }

    }



}
