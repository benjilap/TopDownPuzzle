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

    bool lavaInLerp;
    bool obsiInLerp;

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
            obsiInLerp = true;
            StartCoroutine(LerpElement(meshObsidian, nextObsidianPos));
        }
        if (Vector3.Distance(meshLava.transform.position, nextLavaPos) > 0.1f && !lavaInLerp)
        {
            lavaInLerp = true;
            StartCoroutine(LerpElement(meshLava, nextLavaPos));

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
        }
    }

    IEnumerator LerpElement(GameObject myGameObject, Vector3 myNextPos)
    {
        float saveTime = Time.time;
        while (Vector3.Distance(myGameObject.transform.position, myNextPos) > 0.1f)
        {
            myGameObject.transform.position = Vector3.Lerp(myGameObject.transform.position, myNextPos, Time.time-saveTime);
            if (Vector3.Distance(meshObsidian.transform.position, nextObsidianPos) <= 0.1f)
            {
                meshObsidian.transform.position = nextObsidianPos;
                yield return 0;
            }
        }
    }
}
