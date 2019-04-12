using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {

    [SerializeField]
    Vector3 spawnPos;
    float spawnAngle;

    Object playerPrefab;

	void Start () {
        playerPrefab = Resources.Load("Player/Player");
	}
	
	// Update is called once per frame
	void Update () {
        CheckPlayer();
	}

    void CheckPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, transform.position + spawnPos, Quaternion.Euler(0,spawnAngle,0));
            if (GameManager.nextLevelNum == GameManager.levelNum)
            {
                GameManager.nextLevelNum++;
            }
        }
    }
}
