using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UnlockStairs : MonoBehaviour {

    public bool StairsState;
    
	
	
	void Update () {
        transform.GetChild(1).GetComponent<Animator>().SetBool("StairsState", StairsState);
    }
}
