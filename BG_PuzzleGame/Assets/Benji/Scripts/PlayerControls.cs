using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    [SerializeField]
    float jumpForce=1;
    [SerializeField]
    float playerSpeed=1;

    bool canJump;

    //CheckJumpTimer
    bool startCheckJump;
    float saveTimeCheck;
    float timerCheck;
    float timeToReset = 0.4f;

    Vector3 xAxis;
    Vector3 yAxis;

    GameObject myPivotCamera;


	void Start () {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
	}
	
	void Update () {
        UpdatePlayerAxis();
        PlayerMovement();
        PlayerJump();
        
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
            this.GetComponent<Rigidbody>().velocity = gravity+(((xAxis * Input.GetAxis("Horizontal"))+ (yAxis * Input.GetAxis("Vertical")))).normalized*playerSpeed;

        }

    }

    void PlayerJump()
    {
        if (this.GetComponent<Rigidbody>().velocity.y > -2 && this.GetComponent<Rigidbody>().velocity.y < 2)
        {
            if (Input.GetButtonDown("Jump") && canJump)
            {

                canJump = false;
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0) * jumpForce);

            }
            else if (!canJump)
            {

                if (!startCheckJump)
                {
                    startCheckJump = true;
                    saveTimeCheck = Time.time;
                }
                timerCheck = Time.time - saveTimeCheck;
                if (timerCheck >= timeToReset)
                {
                    startCheckJump = false;
                    canJump = true;
                }
            }
        }
        else
        {
            startCheckJump = false;

        }
    }



}
