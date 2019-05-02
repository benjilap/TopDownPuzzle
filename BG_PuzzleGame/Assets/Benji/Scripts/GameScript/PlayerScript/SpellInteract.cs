using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInteract : MonoBehaviour {

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public string actualElement;

    Object ImpactFX;

    void Start () {
        InstantiateFX();
	}
	
	// Update is called once per frame
	void Update () {
        CheckCollision();
	}

    void CheckCollision()
    {
        RaycastHit ObstacleCollide;
        if (Physics.Raycast(transform.position, this.GetComponent<Rigidbody>().velocity, out ObstacleCollide, 0.2f))
        {
            if (ObstacleCollide.transform.gameObject.tag != "Player")
            {
                if (ObstacleCollide.collider.gameObject.transform.parent != null)
                {
                    if (actualElement == "Water")
                    {
                        Debug.Log(ObstacleCollide.collider.gameObject);

                        if (ObstacleCollide.collider.gameObject.transform.parent != null & ObstacleCollide.collider.gameObject.transform.parent.parent != null)
                        {
                            if (ObstacleCollide.collider.gameObject.transform.parent.parent.GetComponent<LavaElement>() != null || ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>() != null)
                            {
                                if (ObstacleCollide.transform.gameObject.tag != "Obsidian")
                                {

                                    ObstacleCollide.collider.gameObject.transform.parent.parent.GetComponent<LavaElement>().hittedState = 1;

                                }
                                else

                                if (ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>().resetLava)
                                {
                                    ObstacleCollide.collider.gameObject.transform.parent.GetComponent<LavaElement>().resetLava = false;
                                }
                            }
                        }
                    }


                    if (actualElement == "Fire")
                    {

                        if (ObstacleCollide.collider.gameObject.transform.parent.GetComponent<WaterElement>() != null)
                        {

                            ObstacleCollide.collider.gameObject.transform.parent.GetComponent<WaterElement>().geyserState = 1;

                        }

                        if (ObstacleCollide.collider.GetComponent<Torch>() != null)
                        {
                            
                            ObstacleCollide.collider.GetComponent<Torch>().lighted = true;
                        }
                    }

                    if (ObstacleCollide.transform.gameObject.tag != "Water")
                    {
                        GameObject SpellImpact = Instantiate(ImpactFX, this.transform.position, Quaternion.identity) as GameObject;
                        Destroy(this.gameObject,0.5f);
                    }
                }
                else
                {

                }

            }
        }
    }

    void CheckPos()
    {
        if (player != null)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) > 500)
            {
                Destroy(this.gameObject,0.5f);
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

    void InstantiateFX()
    {
        if (Resources.Load("FX/FX_" + actualElement + "_Spell") != null)
        {
            Object ElementFX = Resources.Load("FX/FX_" + actualElement + "_Spell");
            GameObject SpellFX = Instantiate(ElementFX, this.transform.position, Quaternion.identity) as GameObject;
            SpellFX.transform.SetParent(this.transform);
            SpellFX.transform.GetChild(0).localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (Resources.Load("FX/FX_" + actualElement + "_Impact") != null)
        {
            ImpactFX = Resources.Load("FX/FX_" + actualElement + "_Impact");
        }
    }
}
