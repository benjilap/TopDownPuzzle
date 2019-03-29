using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour
{
    public bool activated;
    public GameObject MySprite;

    public void setSprite()
    {
        activated = !activated;
        MySprite.SetActive(activated);
    }

}