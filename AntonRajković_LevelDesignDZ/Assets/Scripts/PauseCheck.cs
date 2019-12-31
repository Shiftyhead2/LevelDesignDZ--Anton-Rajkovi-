using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseCheck : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.GamePaused)
        {
            this.gameObject.GetComponent<FirstPersonController>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<FirstPersonController>().enabled = true;
        }
    }
}
