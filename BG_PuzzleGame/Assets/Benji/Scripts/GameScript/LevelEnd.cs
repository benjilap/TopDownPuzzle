using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject myPlayer = other.gameObject;
            myPlayer.transform.parent.GetComponent<PlayerControls>().canMove=false;
            myPlayer.transform.parent.position = Vector3.Lerp(myPlayer.transform.parent.position, transform.position + new Vector3(0, 1, 0), 0.9f);
            Debug.Log(Vector3.Distance(transform.position, myPlayer.transform.parent.position));
            if (Vector3.Distance(transform.position + new Vector3(0, 1, 0), myPlayer.transform.parent.position) < 0.5f)
            {
                GameManager.levelNum++;
                SceneManager.LoadScene("Level" + GameManager.nextLevelNum);
                
            }
        }
    }
}
