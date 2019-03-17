using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    [SerializeField]
    float jumpForce=1;
    [SerializeField]
    float playerSpeed=1;

    bool canJump;
    [HideInInspector]
    public bool canMove = true;

    //CheckJumpTimer
    bool startCheckJump;
    float saveTimeCheck;
    float timerCheck;
    float timeToReset = 0.2f;

    Vector3 tempPlayerDir = new Vector3(0,0,1);
    Vector3 playerDir;
    Vector3 xAxis;
    Vector3 yAxis;

    GameObject myPivotCamera;


	void Start () {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
	}
	
	void FixedUpdate () {
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
        if (canMove)
        {
            playerDir = ((xAxis * Input.GetAxis("Horizontal")) + (yAxis * Input.GetAxis("Vertical"))).normalized;
            Vector3 gravity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);

            RaycastHit hit;
            Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), playerDir);
            //Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, 40));
            //Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, -40));

            if (!canJump)
            {
                if (!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), playerDir, out hit, tempPlayerDir.magnitude))
                //&& !Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, 40), out hit, tempPlayerDir.magnitude)
                //&&!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, - 40), out hit, tempPlayerDir.magnitude))
                {

                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    {

                        this.GetComponent<Rigidbody>().velocity = gravity + playerDir * playerSpeed;
                        tempPlayerDir = playerDir;
                    }
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {

                    this.GetComponent<Rigidbody>().velocity = gravity + playerDir * playerSpeed;
                    tempPlayerDir = playerDir;
                }
            }


            this.transform.rotation = Quaternion.LookRotation(tempPlayerDir);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {

            canJump = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0) * jumpForce);

        }
        else if (!canJump)
        {
            if (this.GetComponent<Rigidbody>().velocity.y > -1f && this.GetComponent<Rigidbody>().velocity.y < 1f)
            {
                float tempPlayerHeight =0;

                if (!startCheckJump)
                {
                    tempPlayerHeight = this.GetComponent<Rigidbody>().velocity.y;
                    startCheckJump = true;
                    saveTimeCheck = Time.time;
                }
                timerCheck = Time.time - saveTimeCheck;
                if (timerCheck >= timeToReset* this.GetComponent<Rigidbody>().velocity.y+timeToReset)
                {
                    if (tempPlayerHeight == this.GetComponent<Rigidbody>().velocity.y)
                    {
                        startCheckJump = false;
                        canJump = true;

                    }
                    else
                    if (startCheckJump)
                    {
                        startCheckJump = false;
                    }
                }
            }

        }
    }

    Vector3 UpdateVecWallDetect(Vector3 originVector, float angle)
    {
        Vector3 myNewDir = Vector3.zero;
        float vectorAngle = angle;
        float initVectorAngle = Vector3.Angle(Vector3.right, originVector);
        if (initVectorAngle < 0)
        {
            initVectorAngle = initVectorAngle * -1 + 180;
        }
        vectorAngle = initVectorAngle + angle;



        myNewDir = new Vector3((Mathf.Cos(vectorAngle * Mathf.Deg2Rad)) , 0, (Mathf.Sin(vectorAngle * Mathf.Deg2Rad)) );




        Debug.Log(myNewDir);

        return myNewDir;
    }
}
