using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInteract : MonoBehaviour {

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public string actualElement;

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        CheckCollision();
	}

    void CheckCollision()
    {
        RaycastHit ObstacleCollide;
        if (Physics.Raycast(transform.position, this.GetComponent<Rigidbody>().velocity, out ObstacleCollide, 0.2f)){
            if(ObstacleCollide.transform.gameObject.tag != "Player")
            {
                if (actualElement == "Water")
                {
                        Debug.Log(ObstacleCollide.collider.gameObject);
                    if (ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>() != null)
                    {
                        ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>().hittedState = 1;
                        if (ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>().resetLava)
                        {
                            ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>().resetLava = false;
                        }
                    }
                }
                Destroy(this.gameObject);
            }
            else
            {

            }

        }
    }

    void CheckPos()
    {
        if (player != null)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) > 1000)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
