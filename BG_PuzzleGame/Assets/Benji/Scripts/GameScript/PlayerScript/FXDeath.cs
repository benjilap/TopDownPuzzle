using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDeath : MonoBehaviour {

    private void Start()
    {
        Destroy(this.gameObject, this.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration);
    }
}
