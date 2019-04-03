using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInteract : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckCollision();
	}

    void CheckCollision()
    {
        RaycastHit ObstacleCollide;
        if (Physics.Raycast(transform.position, this.GetComponent<Rigidbody>().velocity, out ObstacleCollide, 0.1f)){
            if(ObstacleCollide.transform.gameObject.tag != "Player")
            {
            Destroy(this.gameObject);

            }
            else
            {

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
