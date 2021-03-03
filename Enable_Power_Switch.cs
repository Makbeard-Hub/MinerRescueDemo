using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Small helper script to enable the switch in room 3
 *  that the player needs to flip to exit the level.
 * 
 */

public class Enable_Power_Switch : MonoBehaviour
{
    Powered_Component _powerScript;

    [SerializeField]
    GameObject _attachedLight;

    [SerializeField]
    Material _startingMat;

    bool _activated;

    // Start is called before the first frame update
    void Start()
    {
        _powerScript = GetComponent<Powered_Component>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_powerScript.GetPowerStatus() == true && _activated == false)
        {
            EnableComponents();
        }
    }

    void EnableComponents()
    {
        //Turn the light "on" by setting its material to the deactive (red) light
        MeshRenderer lightRenderer = _attachedLight.GetComponent<MeshRenderer>();
        lightRenderer.material = _startingMat;

        //Activate the switch so it can be toggled
        Power_Switch switchScript = GetComponent<Power_Switch>();
        switchScript.enabled = true;

        //Toggle the activated bool so this activation only occurs once
        _activated = true;
    }
}
