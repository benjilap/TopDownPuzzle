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

    [HideInInspector]
    public Vector3 playerMovement;

    GameObject myPivotCamera;

    [SerializeField]
    float playerYVar ;


    void Start () {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
	}
	
	void FixedUpdate () {
        UpdatePlayerAxis();
        PlayerMovement();
        PlayerJump();

        playerYVar = this.GetComponent<Rigidbody>().velocity.y;

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
                if (!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), tempPlayerDir, out hit, tempPlayerDir.magnitude))
                //&& !Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, 40), out hit, tempPlayerDir.magnitude)
                //&&!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, - 40), out hit, tempPlayerDir.magnitude))
                {

                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    {

                        this.GetComponent<Rigidbody>().velocity = gravity + playerDir * playerSpeed;
                        tempPlayerDir = playerDir;
                        playerMovement = tempPlayerDir;
                    }
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {

                    this.GetComponent<Rigidbody>().velocity = gravity + playerDir * playerSpeed;
                    tempPlayerDir = playerDir;
                    playerMovement = tempPlayerDir;

                }
            }


            this.transform.rotation = Quaternion.LookRotation(tempPlayerDir);
        }
    }

    void PlayerJump()
    {
        Debug.DrawRay(this.transform.position, Vector3.down *0.6f + playerDir / 2, Color.green);

        if (Input.GetButtonDown("Jump") && canJump)
        {

            canJump = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0) * jumpForce);

        }
        else if (!canJump)
        {
            RaycastHit jumpDetect;
            if (Physics.Raycast(this.transform.position, Vector3.down + playerDir / 2, out jumpDetect, 0.6f))
            {

                if (this.GetComponent<Rigidbody>().velocity.y > -0.6f && this.GetComponent<Rigidbody>().velocity.y < 0.6f)
                {
                    canJump = true;
                    Debug.Log("GreenHit");

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
