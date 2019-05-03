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

            GameManager.levelNum = levelNum;
            GameManager.nextLevelNum = GameManager.levelNum + 1;
            SceneManager.LoadScene("Level" + levelNum.ToString());
        }
    }
}
