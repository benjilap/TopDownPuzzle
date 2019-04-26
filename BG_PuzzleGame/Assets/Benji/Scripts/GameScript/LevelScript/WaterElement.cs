using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : MonoBehaviour {

    [SerializeField]
    float timeToStart =3;
    bool geyserStart;

    [SerializeField]
    float timeToBeActive =3;
    bool geyserActive;
    
    float timeSave;
    [HideInInspector]
    public int geyserState;
    [SerializeField]
    float geyserForce=1;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(geyserState);
		if (geyserState == 1)
        {
            GeyserWait(2, timeToStart);
        }else 
        if (geyserState == 2)
        {
            GeyserWait(0, timeToBeActive);
        }
	}

    void GeyserWait(int newGeyserState, float timeToWait)
    {
        if (!geyserStart)
        {
            geyserStart = true;
            timeSave = Time.time;
        }

        if (Time.time >= timeSave + timeToWait)
        {
            geyserState = newGeyserState;
            geyserStart = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (geyserState == 2)
            {
        
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, geyserForce * 1000, 0));
            }
        }
    }
}
