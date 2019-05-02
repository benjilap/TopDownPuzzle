using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWithTorch : MonoBehaviour {

    [SerializeField]
    int functionToUse;
    [SerializeField]
    TorchsSystem TorchesActivated;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TorchesActivated.lightedTorches)
        {
            if (functionToUse == 0)
            {
                UseAtor();
            }
            else
        if (functionToUse == 1)
            {
                EnableEndLevel();
            }
        }
	}

    void UseAtor()
    {
        this.GetComponent<Animator>().SetBool("OpenStairs", true);
    }

    void EnableEndLevel()
    {
        this.enabled = true;
    }

}
