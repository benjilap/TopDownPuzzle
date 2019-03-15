using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasScript : MonoBehaviour {

    public void ChangeMenuFrame(int frameInd)
    {
        this.GetComponent<Animator>().SetInteger("MenuCanvasFrame", frameInd);
    }

    public void LoadLevel(int levelNum)
    {
        if (levelNum == 0)
        {
            Application.Quit();
        }else
        {
            SceneManager.LoadScene("Level" + levelNum.ToString());
            GameManager.levelNum = GameManager.startLevelNum;
            GameManager.nextLevelNum = GameManager.levelNum + 1;
        }
    }
}
