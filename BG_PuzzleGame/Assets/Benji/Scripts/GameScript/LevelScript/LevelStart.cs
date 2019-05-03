using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {

    [SerializeField]
    Vector3 spawnPos;
    [SerializeField]
    float spawnAngle;

    Object playerPrefab;
    Object playerCanvas;
    Object pauseCanvas;

    void Start () {
        playerPrefab = Resources.Load("Player/Player");
        playerCanvas = Resources.Load("Player/PlayerCanvas");
        pauseCanvas = Resources.Load("Menu/PauseCanvas");
    }
	
	// Update is called once per frame
	void Update () {
        CheckPauseCanvas();
        CheckPlayerCanvas();
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

    void CheckPlayerCanvas()
    {
        if (GameObject.FindObjectOfType<PlayerCanvasScript>() == null)
        {
            Instantiate(playerCanvas, Vector3.zero, Quaternion.identity);

        }
    }

    void CheckPauseCanvas()
    {
        if (GameObject.FindObjectOfType<PauseCanvasScript>() == null)
        {
            Instantiate(pauseCanvas, Vector3.zero, Quaternion.identity);

        }

    }
}
