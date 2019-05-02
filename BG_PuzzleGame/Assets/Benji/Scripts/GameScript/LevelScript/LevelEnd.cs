using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    bool startLerp;
    float saveTime;
    float timeVar;

    Vector3 startPos;

    GameObject myPlayer;

    void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myPlayer = other.gameObject;
            if (!startLerp)
            {
                startLerp = true;
                saveTime = Time.time;
                myPlayer.transform.GetComponent<PlayerControls>().canMove=false;
                startPos = myPlayer.transform.position;
            }
            timeVar = Time.time - saveTime;
            myPlayer.transform.position = Vector3.Lerp(startPos + myPlayer.transform.GetComponent<PlayerControls>().playerMovement, transform.position + new Vector3(0, 1, 0), timeVar*2.5f);
            if (Vector3.Distance(transform.position + new Vector3(0, 1, 0), myPlayer.transform.position) < 0.1f)
            {
                GameManager.levelNum++;
                if (Application.CanStreamedLevelBeLoaded("Level" + GameManager.nextLevelNum))
                {

                    SceneManager.LoadScene("Level" + GameManager.nextLevelNum);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("EndCanvas").transform.GetChild(0).GetComponent<Animator>().SetBool("EndGame",true);
                }

            }
        }
    }
}
