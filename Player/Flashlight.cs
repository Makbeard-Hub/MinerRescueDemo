using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling flashlight behavior
//Attach to light objects that act as flashlights
//
//Created 11/13/2019 by A. Jayasinghe
public class Flashlight : MonoBehaviour
{

    Light _flashlight;

    private void Start()
    {
        _flashlight = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update()
    {
        //When 'F' is hit, toggle the flashlight to the opposite
        //of its current state
        if (Input.GetKeyDown(KeyCode.F))
        {
            _flashlight.enabled = !_flashlight.enabled;
        }
    }
}
