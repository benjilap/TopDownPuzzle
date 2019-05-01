using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchsSystem : MonoBehaviour {

    [HideInInspector]
    public bool lightedTorches;

    List<Torch> Torches = new List<Torch>();

	void Start () {
        SetTorchesSys();
	}
	
	// Update is called once per frame
	void Update () {
        CheckTorches();
	}

    void SetTorchesSys()
    {
        for(int i=0; i<this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<Torch>() != null)
            {
                Torches.Add(this.transform.GetChild(i).GetComponent<Torch>());
            }
        }
    }

    void CheckTorches()
    {
        int lightedCheck = 0;
        for (int i = 0; i < Torches.Count; i++)
        {
            if (Torches[i].lighted)
            {
                lightedCheck +=1;
            }
        }

        if (lightedCheck == Torches.Count)
        {
            lightedTorches = true;
        }
    }
}
