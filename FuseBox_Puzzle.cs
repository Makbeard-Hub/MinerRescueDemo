using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Puzzle is to get all fuses to light green in the box.
 * Adjacent fuses will flip to opposite state when a fuse is activated
 * Modified from Mole-Puzzle
 * When all fuses are "green" "powered" this disables player interaction with them
 * 
 * Script created 5/27/2020 by M. Justice
 * */

public class FuseBox_Puzzle : MonoBehaviour
{
    [SerializeField] GameObject[] allFuses;
    FuseUnit_State fuseUnit;

    private bool powerIsConnected;
    bool isInteractable;

    float checkTimer;
    [SerializeField] float powerCheckTime = 1f;

    //Audio
    [SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip buttonPressSound;
    [SerializeField] AudioClip poweredCorrectlySound;


    // Start is called before the first frame update
    void Start()
    {
        checkTimer = 0f;
        powerIsConnected = false;
        isInteractable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isInteractable)
        {
            return;
        }

        if(other.gameObject.tag == "Player" && checkTimer >= powerCheckTime)
        {
            CheckForConnectedPower();
            checkTimer = 0f;
            if (powerIsConnected)
            {
                isInteractable = false;
                LockDownSystem();
            }
        }
        checkTimer += Time.deltaTime;
    }

    private void CheckForConnectedPower()
    {       
        foreach (GameObject fuse in allFuses)
        {
            fuseUnit = fuse.GetComponent<FuseUnit_State>();
            if (!fuseUnit.GetPowerState())
            {
                powerIsConnected = false;
                break;
            }
            else
            {
                powerIsConnected = true;
            }
        }
    }

    public bool GetPowerState()
    {
        return powerIsConnected;
    }

    private void LockDownSystem()
    {
        foreach (GameObject fuse in allFuses)
        {
            fuseUnit = fuse.GetComponent<FuseUnit_State>();
            fuseUnit.LockDown(true);
        }
    }

    private void UnlockSystem()
    {
        foreach (GameObject fuse in allFuses)
        {
            fuseUnit = fuse.GetComponent<FuseUnit_State>();
            fuseUnit.LockDown(false);
        }
    }

    private void OnMouseOver()
    {
    }

    private void OnMouseExit()
    {
    }
}