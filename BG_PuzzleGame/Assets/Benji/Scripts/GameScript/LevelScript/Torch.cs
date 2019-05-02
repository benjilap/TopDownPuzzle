using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    [SerializeField]
    float enlightedTime;
    [HideInInspector]
    public bool lighted;
    bool beEnlighted;
    float saveTime;

    ParticleSystem Fire;
    GameObject fireLight;

    private void Start()
    {
        fireLight = this.transform.GetChild(2).gameObject;
        Fire = this.transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (enlightedTime != 0)
        {
            EnlightedTime();
        }

        if (lighted)
        {
            Fire.Play();
            fireLight.SetActive(true);
        }

    }

    void EnlightedTime()
    {
        if (lighted)
        {
            if (!beEnlighted)
            {
                saveTime = Time.time;
                beEnlighted = true;
            }
            if (Time.time >= saveTime + enlightedTime)
            {
                beEnlighted = false;
                lighted = false;
                Fire.Stop();
                fireLight.SetActive(false);

            }
        }
    }
}
