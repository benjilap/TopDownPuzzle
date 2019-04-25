using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    [SerializeField]
    float jumpForce=1;
    [SerializeField]
    float playerSpeed=1;

    bool canJump;
    bool hasJump;
    [HideInInspector]
    public bool canMove = true;
    Vector3 climbStairs;


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

    [SerializeField]
    float spellForce;
    int useSpellState;
    GameObject spellSpawner;
    GameObject spellPrefab;
    GameObject spellCasted;
    [SerializeField]
    float ySpellOffset=3;
    float launchYSpellOffset;
    [SerializeField]
    float rangeSpell = 5;
    float launchRangeSpell;
    Vector3 spellDir;
    Vector3 spellOffsetPlayer;
    [SerializeField]
    static float saveSpellTimer;
    bool startTimer;
    bool resetTimer;
    bool launchSpellTimer;
    bool upSpell;

    [SerializeField]
    float upOffset=1;
    float maxSpellHeight;
    float minSpellHeight;
    float maxSpellRange;
    float minSpellRange;
    Vector3 initScale;

    public string[] spellName = new string[1];
    List<SpellOrder> spellList = new List<SpellOrder>();
    int actualSpell =1;

    void Start()
    {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
        charAtor = this.transform.GetChild(0).GetComponent<Animator>();
        charPos = this.transform.GetChild(0).localPosition;
        charAngle = this.transform.GetChild(0).localRotation;
        charScale = this.transform.GetChild(0).localScale;
        spellSpawner = charAtor.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/SpellPoint").gameObject;
        spellPrefab = Resources.Load<GameObject>("Player/SpellPrefab");
        spellName = new string[2];

        spellName.SetValue("Water", 0);
        spellName.SetValue("Fire", 1);
        SetSpell();

    }
	
	void FixedUpdate () {
        UpdatePlayerAxis();

        PlayerMovement();
        PlayerJump();
        MoveAnimControl();
        if (canJump)
        {
            CastSpell();
        }
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

        playerDir = ((xAxis * Input.GetAxis("Horizontal")) + (yAxis * Input.GetAxis("Vertical")));
        playerDirNorm = playerDir.normalized;
        Vector3 gravity = new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);
        RaycastHit hit;
        Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), playerDirNorm * 0.4f);
        //Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, 40));
        //Debug.DrawRay(this.transform.position + new Vector3(0, -0.4f, 0), UpdateVecWallDetect(tempPlayerDir, -40));
        if (canMove)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {

                if (!canJump)
                {
                    if (!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), playerDirNorm, out hit, tempPlayerDir.magnitude * 0.4f) & !Physics.Raycast(this.transform.position + new Vector3(0, 0.4f, 0), playerDirNorm, out hit, tempPlayerDir.magnitude * 0.5f))
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


            }
            else if (Input.GetAxis("Horizontal") == 0 & Input.GetAxis("Vertical") == 0)
            {
                if (this.GetComponent<Rigidbody>().velocity.y < 0)
                {
                    if (canJump && !hasJump)
                    {
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

                    }
                }
            }
        }
    }


    void PlayerJump()
    {

        Debug.DrawRay(this.transform.position, Vector3.down * 0.9f + playerDirNorm * 0.05f, Color.green);

        RaycastHit jumpDetect;
        RaycastHit wallDetect;
        if (Physics.Raycast(this.transform.position, Vector3.down + playerDirNorm * 0.05f, out jumpDetect, 0.9f))
        {
            canJump = true;
            hasJump = false;

            if (!Physics.Raycast(this.transform.position + new Vector3(0, -0.4f, 0), playerDirNorm, out wallDetect, tempPlayerDir.magnitude * 0.4f) & !Physics.Raycast(this.transform.position + new Vector3(0, 0.4f, 0), playerDirNorm, out wallDetect, tempPlayerDir.magnitude * 0.5f))
            {

                if (this.GetComponent<Rigidbody>().velocity.y > -0.6f && this.GetComponent<Rigidbody>().velocity.y < 0.6f)
                {
                    if (Input.GetButtonDown("Jump") && useSpellState==0)
                    {
                        hasJump = true;
                        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0) * jumpForce);
                    }

                }

            }

        }
        else
        {
            canJump = false;
        }
    }


    void CastSpell()
    {

        //Debug.Log(Input.GetAxis("FireSpell"));
        if (Input.GetAxis("FireSpell") > 0)
        {
            if (spellCasted == null& !launchSpellTimer)
            {
                if (useSpellState == 2)
                {
                    if (SpellTimer(startTimer, 0.2f))
                    {
                        startTimer = false;
                        spellCasted = Instantiate(spellPrefab, spellSpawner.transform.position, Quaternion.identity);
                        spellCasted.transform.SetParent(spellSpawner.transform);
                        spellCasted.GetComponent<SpellInteract>().player = this.gameObject;
                        launchRangeSpell = minSpellRange;
                        launchYSpellOffset = minSpellHeight;
                        initScale = spellCasted.transform.localScale;
                        for (int i = 0; i < spellList.Count; i++)
                        {
                            if (spellList[i].ReturnNameForID(actualSpell) != null)
                            {

                                spellCasted.GetComponent<SpellInteract>().actualElement = spellList[i].ReturnNameForID(actualSpell);
                            }
                        }
                    }
                    else
                    {
                        startTimer = true;
                    }
                }else
                if (SpellTimer(startTimer, 0.3f))
                {

                    startTimer = false;
                    useSpellState = 2;
                }
                else
                {
                    if (useSpellState == 0)
                    {
                        useSpellState = 1;
                    }
                    resetTimer = false;
                    startTimer = true;
                    canMove = false;
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;

                }
            }
            if (spellCasted != null)
            {
                if (launchRangeSpell < maxSpellRange)
                {
                    if (SpellTimer(upSpell, 0.1f))
                    {


                        UpSpell(spellCasted);
                        upSpell = false;
                    }
                    else
                    {
                        upSpell = true;
                    }
                }
            }

        }
        else if (saveSpellTimer != 0)
        {

            if (useSpellState < 2)
            {
                if (spellCasted != null)
                {
                    Destroy(spellCasted);
                }
                if (SpellTimer(resetTimer, 0.2f))
                {
                    resetTimer = false;
                    useSpellState = 0;
                    canMove = true;
                    startTimer = false;
                }
                else
                {
                    resetTimer = true;
                }
            }
            else
            {
                if (spellCasted != null)
                {
                    useSpellState = 3;
                }

                if (SpellTimer(launchSpellTimer, 0.4f))
                {
                    if (spellCasted != null)
                    {
                        spellCasted.transform.parent = null;
                        spellCasted.GetComponent<Rigidbody>().AddForce(UpdateSpellPlayerOffset());
                        spellCasted.GetComponent<Rigidbody>().useGravity = true;
                        spellCasted = null;

                    }
                    useSpellState = 0;
                    launchSpellTimer = false;
                }
                else
                {
                    launchSpellTimer = true;

                }
            }

        }
    }

    void UpSpell(GameObject castedSpell)
    {
        if (castedSpell.transform.localScale.x < initScale.x*3)
        {


            castedSpell.transform.localScale += new Vector3((initScale.x *2/100 )* Time.deltaTime, (initScale.x * 2 / 100) * Time.deltaTime, (initScale.x * 2 / 100) * Time.deltaTime) * upOffset;
        }
        launchRangeSpell += (((maxSpellRange - minSpellRange)/100) * Time.deltaTime) * upOffset;
        launchYSpellOffset += (((maxSpellHeight - minSpellHeight) /100)*Time.deltaTime) * upOffset;


    }

    void SetSpell()
    {
        for (int i = 0; i < spellName.Length; i++)
        {
            SpellOrder newSpell = new SpellOrder();
            newSpell.UpdateSpell(spellName[i], i );
            spellList.Add(newSpell);
        }
        maxSpellHeight = ySpellOffset;
        maxSpellRange = rangeSpell;
        minSpellHeight = 1;
        minSpellRange = 1;
    } 
    

    static bool SpellTimer(bool spellCast,float timeToWait)
    {
        bool spellState = false;
        if (!spellCast)
        {

            spellCast = true; 
            saveSpellTimer = Time.time;
        }
        if (Time.time >= saveSpellTimer+ timeToWait)
        {

            spellCast = false;
            return spellState = true;

        }
        else
        {

            return spellState =false;

        }

    }

    void MoveAnimControl()
    {
        //MoveAnim
        if (canMove)
        {
            charAtor.SetFloat("MovmentVar", playerDir.magnitude);
        }
        charAtor.gameObject.transform.localPosition = charPos;
        charAtor.gameObject.transform.localRotation = charAngle;
        charAtor.gameObject.transform.localScale = charScale;
        
        //JumpAnim
        charAtor.SetBool("HasJump", hasJump);
        charAtor.SetBool("IsFalling", canJump);

        //RotationControl
        if (playerDirNorm.magnitude != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(playerDirNorm);
            tempPlayerDir = playerDirNorm;
        }
        else
        {
            this.transform.rotation = Quaternion.LookRotation(tempPlayerDir);
        }
        this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);

        //SpellCastAnim
        
        charAtor.SetInteger("UseSpellState", useSpellState);
        

    }

    void StopSliding()
    {
        if (canJump == true)
        {
            if (Input.GetAxis("Horizontal") == 0 & Input.GetAxis("Vertical") == 0)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;

            }
        }
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

    Vector3 UpdateSpellPlayerOffset()
    {
        Vector3 newSpellForce = Vector3.zero;
        spellDir = new Vector3(0, launchYSpellOffset, 0);
        newSpellForce = (((tempPlayerDir * launchRangeSpell + this.transform.position) - spellSpawner.transform.position)+spellDir)*spellForce;
        return newSpellForce;
    }

}

public class SpellOrder
{
    
    string spellName;
    int spellID;

    public void UpdateSpell(string newSpellName,int newSpellID)
    {
        spellName = newSpellName;
        spellID = newSpellID;
    }

    public string ReturnNameForID(int actualSpell)
    {
        if(actualSpell == spellID)
        {
            return spellName;

        }
        else
        {
            return null;
        }
    }
}
