using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaElement : MonoBehaviour {

    [HideInInspector]
    public int hittedState;

    GameObject meshLava;
    GameObject meshObsidian;
    Vector3 pos0State;
    Vector3 pos1State;
    Vector3 nextLavaPos;
    Vector3 nextObsidianPos;

    [SerializeField]
    float timeToReset = 10;
    [HideInInspector]
    public bool resetLava;
    bool lavaInLerp;
    bool obsiInLerp;
    static float saveTimeLerp;
    static float saveTimeReset;

    private void Start()
    {
        meshLava = this.transform.GetChild(0).gameObject;
        meshObsidian = this.transform.GetChild(1).gameObject;
        pos0State = meshLava.transform.position;
        pos1State = meshObsidian.transform.position;
    }

    private void Update()
    {
        SetNextPos();
        if (Vector3.Distance(meshObsidian.transform.position, nextObsidianPos) > 0.1f && !obsiInLerp)
        {
            LerpElement(meshObsidian, nextObsidianPos);
        }else
        if (Vector3.Distance(meshLava.transform.position, nextLavaPos) > 0.1f && !lavaInLerp)
        {
            LerpElement(meshLava, nextLavaPos);

        }
    }

    void SetNextPos()
    {

        if (hittedState == 0)
        {
            nextLavaPos = pos0State;
            nextObsidianPos = pos1State;
        }
        else if(hittedState == 1)
        {
            nextLavaPos = pos1State;
            nextObsidianPos = pos0State;
            ResetLava(timeToReset);
        }

        
    }

    void LerpElement(GameObject myGameObject, Vector3 myNextPos)
    {
        if (saveTimeLerp == 0)
        {
        saveTimeLerp = Time.time;

        }

        //while (Vector3.Distance(myGameObject.transform.position, myNextPos) > 0.1f)
        if(Vector3.Distance(myGameObject.transform.position, myNextPos) > 0.1f)
        {

            myGameObject.transform.position = Vector3.Lerp(myGameObject.transform.position, myNextPos, (Time.time-saveTimeLerp)*0.2f);
            if (Vector3.Distance(myGameObject.transform.position, myNextPos) <= 0.1f)
            {
                myGameObject.transform.position = myNextPos;
                saveTimeLerp = 0;
                

            }
        }
        

    }

    void ResetLava(float timeToWait)
    {

        
        if (!resetLava)
        {
            resetLava = true;
            saveTimeReset = Time.time;
        }

        if (Time.time>= saveTimeReset + timeToWait)
        {
            hittedState = 0;
            resetLava = false;
        }
    }
}
