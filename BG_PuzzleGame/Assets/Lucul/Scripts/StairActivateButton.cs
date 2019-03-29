using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairActivateButton : MonoBehaviour
{




    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetInteger("ButtonState", 1);
            transform.parent.GetComponent<UnlockStairs>().StairsState = 1;
        }
    }
}
