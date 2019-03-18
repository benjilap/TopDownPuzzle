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
                myPlayer.transform.parent.GetComponent<PlayerControls>().canMove=false;
                startPos = myPlayer.transform.parent.position;
            }
            timeVar = Time.time - saveTime;
            myPlayer.transform.parent.position = Vector3.Lerp(startPos + myPlayer.transform.parent.GetComponent<PlayerControls>().playerMovement, transform.position + new Vector3(0, 1, 0), timeVar*2.5f);
            if (Vector3.Distance(transform.position + new Vector3(0, 1, 0), myPlayer.transform.parent.position) < 0.1f)
            {
                GameManager.levelNum++;
                SceneManager.LoadScene("Level" + GameManager.nextLevelNum);
                
            }
        }
    }
}
