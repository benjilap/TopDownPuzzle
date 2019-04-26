using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour {

    [SerializeField]
    Vector3 playerOffset = new Vector3(0,1,0);

    GameObject spellBarInd;
    GameObject actualPlayer;

	void Start () {
        spellBarInd = GameObject.Find("SpellPowerInd");
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSpellBarPos();
	}

    void UpdateSpellBarPos()
    {
        Vector3 newBarPos = Camera.main.WorldToScreenPoint(Camera.main.transform.parent.GetComponent<CameraControls>().currentTarget.transform.position + playerOffset);

        spellBarInd.transform.position = newBarPos;
    }
}
