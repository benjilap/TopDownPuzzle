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

    private void Update()
    {
        if (enlightedTime != 0)
        {
            EnlightedTime();
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
            }
        }
    }
}
