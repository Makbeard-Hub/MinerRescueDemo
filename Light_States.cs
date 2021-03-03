using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling the colors of lights attached to powered components
//
//Created 11/22/2019 by A. Jayasinghe

public class Light_States : MonoBehaviour
{
    public Material poweredMat;
    public Material unpoweredMat;
    [SerializeField] Light lightSource;

    MeshRenderer lightRenderer;

    private void Start()
    {
        lightRenderer = GetComponent<MeshRenderer>();
        lightSource.color = Color.red;
    }
    public void SetPowerStatus(bool state)
    {
        if(state == true)
        {
            lightRenderer.material = poweredMat;
            lightSource.color = Color.green;
        }
        else
        {
            lightRenderer.material = unpoweredMat;
            lightSource.color = Color.red;
        }
    }
}
