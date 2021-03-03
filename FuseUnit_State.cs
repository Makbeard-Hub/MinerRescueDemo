using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fuses power state controlled here. Refers to FuseBoxPuzzle script
//
// Script created 5/27/2020 by M. Justice
public class FuseUnit_State : MonoBehaviour
{
    public bool systemLocked = false;

    [SerializeField] bool isPowered;
    [SerializeField] Color poweredColor, unpoweredColor;
    [SerializeField] GameObject[] neighbors;

    //Audio
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        if(isPowered)
        {
            GetComponent<Renderer>().material.SetColor("_Color", poweredColor);

        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", unpoweredColor);
        }
    }

    public void LockDown(bool state)
    {
        systemLocked = state;
    }

    public bool GetPowerState()
    {
        return isPowered;
    }

    public void SwapFuseState()
    {
        isPowered = !isPowered;
        if (isPowered)
        {
            GetComponent<Renderer>().material.SetColor("_Color", poweredColor);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", unpoweredColor);
        }

        audioSource.Play();
    }

    void SwapNeighborsStates()
    {
        foreach (GameObject nFuse in neighbors)
        {
            FuseUnit_State fuseUnit_State = nFuse.GetComponent<FuseUnit_State>();
            if (!fuseUnit_State.systemLocked)
            {
                fuseUnit_State.SwapFuseState();
            }
        }
    }

    private void OnMouseDown()
    {
        if (systemLocked)
        {
            return;
        }
        SwapFuseState();
        SwapNeighborsStates();
    }
}