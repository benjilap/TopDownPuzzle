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

    private void Start()
    {
        Fire = this.transform.GetChild(0).GetComponent<ParticleSystem>();
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
            }
        }
    }
}
