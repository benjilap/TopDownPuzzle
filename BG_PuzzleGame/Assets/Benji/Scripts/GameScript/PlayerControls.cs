using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    [SerializeField]
    float jumpForce=1;
    [SerializeField]
    float playerSpeed=1;

    bool canJump = true;
    [HideInInspector]
    public bool canMove = true;

    //CheckJumpTimer
    bool startCheckJump;
    float saveTimeCheck;
    float timerCheck;
    float timeToReset = 0.2f;

    Vector3 tempPlayerDir = new Vector3(0,0,1);
    Vector3 playerDir;
    Vector3 playerDirNorm;
    Vector3 xAxis;
    Vector3 yAxis;

    [HideInInspector]
    public Vector3 playerMovement;

    GameObject myPivotCamera;

    Animator charAtor;
    Vector3 charPos;
    Quaternion charAngle;
    Vector3 charScale;



    void Start () {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
        charAtor = this.transform.GetChild(1).GetComponent<Animator>();
        charPos = this.transform.GetChild(1).localPosition;
        charAngle = this.transform.GetChild(1).localRotation;
        charScale = this.transform.GetChild(1).localScale;

    }
	
	void FixedUpdate () {
        UpdatePlayerAxis();
        PlayerMovement();
        PlayerJump();
        AnimControl();

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
            playerDir = ((xAxis * Input.GetAxis("Horizontal")) + (yAxis * Input.GetAxis("Vertical")));
            playerDirNorm = playerDir.normalized;
            Vector3 gravity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);

            RaycastHit hit;
            Debug.DrawRay(this.transform.position + new Vector3(0, -0.2f, 0), playerDirNorm * 0.5f);
            //Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, 40));
            //Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, -40));
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {

                if (!canJump)
                {
                    if (!Physics.Raycast(this.transform.position + new Vector3(0, -0.2f, 0), playerDirNorm, out hit, tempPlayerDir.magnitude*0.5f) & !Physics.Raycast(this.transform.position + new Vector3(0, 0.4f, 0), playerDirNorm, out hit, tempPlayerDir.magnitude * 0.5f))
                    //&& !Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, 40), out hit, tempPlayerDir.magnitude)
                    //&&!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, - 40), out hit, tempPlayerDir.magnitude))
                    {
                        this.GetComponent<Rigidbody>().velocity = gravity + ScalePlayerMove(playerDir) * playerSpeed;
                        tempPlayerDir = playerDirNorm;
                        playerMovement = tempPlayerDir;

                    }
                }
                else
                {
                    this.GetComponent<Rigidbody>().velocity = gravity + ScalePlayerMove(playerDir) * playerSpeed;
                    tempPlayerDir = playerDirNorm;
                    playerMovement = tempPlayerDir;

                }

                this.transform.rotation = Quaternion.LookRotation(tempPlayerDir);
            }

        }
    }

    void PlayerJump()
    {
        Debug.DrawRay(this.transform.position, Vector3.down *0.9f + playerDirNorm / 2, Color.green);

        if (Input.GetButtonDown("Jump") && canJump)
        {

            canJump = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0) * jumpForce);

        }
        else if (!canJump)
        {
            RaycastHit jumpDetect;
            RaycastHit wallDetect;
            if (Physics.Raycast(this.transform.position, Vector3.down + playerDirNorm / 2, out jumpDetect, 0.9f))
            {
                if (!Physics.Raycast(this.transform.position + new Vector3(0, -0.2f, 0), playerDirNorm, out wallDetect, tempPlayerDir.magnitude * 0.5f) & !Physics.Raycast(this.transform.position + new Vector3(0, 0.4f, 0), playerDirNorm, out wallDetect, tempPlayerDir.magnitude * 0.5f))
                {
                    if (this.GetComponent<Rigidbody>().velocity.y > -0.6f && this.GetComponent<Rigidbody>().velocity.y < 0.6f)
                    {
                        canJump = true;
                        Debug.Log("GreenHit");

                    }
                }

            }
        }
    }

    void AnimControl()
    {
        charAtor.SetFloat("MovmentVar", playerDir.magnitude);
        charAtor.gameObject.transform.localPosition = charPos;
        charAtor.gameObject.transform.localRotation = charAngle;
        charAtor.gameObject.transform.localScale = charScale;

    }

    Vector3 ScalePlayerMove(Vector3 currentPlayerDir)
    {
        Vector3 newPlayerDir = currentPlayerDir;
        if (currentPlayerDir.magnitude > 0.1f && currentPlayerDir.magnitude < 0.5f)
        {
            newPlayerDir = currentPlayerDir.normalized * 0.5f;
        }
        else if (currentPlayerDir.magnitude >= 0.5f)
        {
            newPlayerDir = currentPlayerDir.normalized;

        }        
        else
        {
            newPlayerDir = Vector3.zero;

        }

        return newPlayerDir;
    }

   
}
