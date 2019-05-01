using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvasScript : MonoBehaviour {

    Animator pauseAtor;

    bool pauseState;
    

    
    // Use this for initialization
	void Start () {
        pauseAtor = this.transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        PauseInput();
        EnablePauseMenu();
	}

    void EnablePauseMenu()
    {

        pauseAtor.SetBool("PauseEnable", pauseState);
        if (GameObject.FindObjectOfType<PlayerControls>())
        {
            GameObject.FindObjectOfType<PlayerControls>().playerEnable = !pauseState;

        }
    }

    void PauseInput()
    {
        if (Input.GetButtonUp("Pause"))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        pauseState = !pauseState;

    }

    public void RestartLevel(GameObject button)
    {

        GameObject.FindObjectOfType<PlayerControls>().isDead = true;
        Destroy(GameObject.FindObjectOfType<PlayerControls>().gameObject,1);
        button.GetComponent<Animator>().SetBool("Normal", true);
        ChangePauseState();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
