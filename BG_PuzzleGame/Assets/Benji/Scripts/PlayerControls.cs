using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    Vector3 xAxis;
    Vector3 yAxis;

    GameObject myPivotCamera;


	void Start () {
        myPivotCamera = GameObject.FindObjectOfType<CameraControls>().gameObject;
	}
	
	void Update () {
        UpdatePlayerAxis();

	}

    void UpdatePlayerAxis()
    {
        
        if ((3*Mathf.PI)/2 <= myPivotCamera.transform.eulerAngles.y && myPivotCamera.transform.eulerAngles.y < 2*Mathf.PI )
        {
            xAxis = new Vector3(Mathf.Cos(myPivotCamera.transform.rotation.y), transform.position.y, -Mathf.Sin(myPivotCamera.transform.rotation.y));
            yAxis = new Vector3(Mathf.Cos(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)), transform.position.y, -Mathf.Sin(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)));
            Debug.Log("4");
        }else
        if (Mathf.PI <= myPivotCamera.transform.eulerAngles.y && myPivotCamera.transform.eulerAngles.y < (3*Mathf.PI)/2 )
        {
            xAxis = new Vector3(-Mathf.Cos(myPivotCamera.transform.rotation.y), transform.position.y, -Mathf.Sin(myPivotCamera.transform.rotation.y));
            yAxis = new Vector3(-Mathf.Cos(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)), transform.position.y, -Mathf.Sin(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)));
            Debug.Log("3");
        }else
        if (Mathf.PI/2 <= myPivotCamera.transform.eulerAngles.y && myPivotCamera.transform.eulerAngles.y < Mathf.PI)
        {
            xAxis = new Vector3(-Mathf.Cos(myPivotCamera.transform.rotation.y), transform.position.y, Mathf.Sin(myPivotCamera.transform.rotation.y));
            yAxis = new Vector3(-Mathf.Cos(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)), transform.position.y, Mathf.Sin(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)));
            Debug.Log("2");
        }else
        if (Mathf.PI/2 <= myPivotCamera.transform.eulerAngles.y && myPivotCamera.transform.eulerAngles.y < 2 * Mathf.PI)
        {
        
            xAxis = new Vector3(Mathf.Cos(myPivotCamera.transform.rotation.y), transform.position.y, Mathf.Sin(myPivotCamera.transform.rotation.y));
            yAxis = new Vector3(Mathf.Cos(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)), transform.position.y, Mathf.Sin(myPivotCamera.transform.rotation.y + (Mathf.PI / 2)));
            Debug.Log("1");
        }
        Debug.Log(myPivotCamera.transform.eulerAngles.y);
        Debug.Log(Mathf.Cos(Mathf.PI));


        Debug.DrawLine(transform.position, xAxis, Color.red);
        Debug.DrawLine(transform.position, yAxis, Color.blue);
    }


}
