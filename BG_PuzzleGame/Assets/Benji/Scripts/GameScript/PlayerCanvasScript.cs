using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour {

    [SerializeField]
    Vector3 playerOffset = new Vector3(0,1,0);

    GameObject spellBarInd;
    GameObject spellBarContent;
    Vector2 startBarPos;

    GameObject actualPlayer;

	void Start () {
        spellBarInd = GameObject.Find("SpellPowerInd");
        spellBarContent = spellBarInd.transform.GetChild(0).GetChild(0).gameObject;
        startBarPos = spellBarContent.GetComponent<RectTransform>().offsetMin;
	}
	
	// Update is called once per frame
	void Update () {
        CheckPlayer();

        if (actualPlayer != null)
        {
            UpdateSpellBarPos();
            UpdateBarAnim();
            UpdateBarPourcent();
            CheckPlayerDeath();
        }
    }

    void CheckPlayer()
    {
        if (actualPlayer == null)
        {
            actualPlayer = GameObject.FindGameObjectWithTag("Player");

        }
    }

    void UpdateSpellBarPos()
    {
        Vector3 newBarPos = Camera.main.WorldToScreenPoint(actualPlayer.transform.position + playerOffset);
        spellBarInd.transform.position = newBarPos;
    }

    void UpdateBarPourcent()
    {
        spellBarContent.GetComponent<RectTransform>().offsetMax = new Vector2(startBarPos.x-((startBarPos.x / 100) * actualPlayer.GetComponent<PlayerControls>().spellPowerPourcent), 0);
        spellBarContent.GetComponent<RectTransform>().offsetMin = new Vector2(startBarPos.x-((startBarPos.x / 100) * actualPlayer.GetComponent<PlayerControls>().spellPowerPourcent), 0);
    }

    void UpdateBarAnim()
    {
        if (actualPlayer.GetComponent<PlayerControls>().useSpellState != 0)
        {
            spellBarInd.GetComponent<Animator>().SetBool("CastSpell", true);
        }
        else
        {
            spellBarInd.GetComponent<Animator>().SetBool("CastSpell", false);
        }
    }

    void CheckPlayerDeath()
    {

            this.transform.Find("DeathScreen").GetComponent<Animator>().SetBool("PlayerDeath", actualPlayer.GetComponent<PlayerControls>().isDead);

    }

}
