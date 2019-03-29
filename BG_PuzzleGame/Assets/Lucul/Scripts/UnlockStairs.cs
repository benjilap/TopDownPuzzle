using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UnlockStairs : MonoBehaviour {

    public int StairsState;
    
	
	
	void Update () {
        transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Animator>().SetInteger("Door_State", DoorState);
    }
}
