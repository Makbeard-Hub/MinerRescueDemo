using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This class should open the water valve that controls specific group of water levels.
 * 
 * Created by M. Justice 08/15/2020
 * */

public class Water_Controls : MonoBehaviour
{
    [SerializeField] GameObject waterValve;
    [SerializeField] GameObject waterValvePickup;
    [SerializeField] bool valveFound = false;
    [SerializeField] bool valveInstalled = false;
    [SerializeField] bool valveOpenAndActivated = false;
    [SerializeField] float mouseOverRange = 5f;
    [SerializeField] Text text;

    [SerializeField] GameObject[] waterRaised;
    [SerializeField] GameObject waterLowered;

    //Audio
    [SerializeField] AudioSource turnValveSFX;
    [SerializeField] AudioSource placeValveInPipeSFX;
    [SerializeField] AudioSource waterRushingSFX;
    [SerializeField] float playDelay;

    Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = waterValve.GetComponent<Animator>();
        foreach (var water in waterRaised)
        {
            water.SetActive(true);
        }

        if (waterLowered != null)
        {
            waterLowered.SetActive(false);
        }

        if (!valveInstalled)
        {
            waterValve.SetActive(false);
        }

        if(valveInstalled == false && waterValvePickup == null)
        {
            Debug.LogError("Missing Valve Pickup GameObject for uninstalled Pipes!");
        }
    }

    private void OnMouseOver()
    {
        if (!valveInstalled && Vector3.Distance(gameObject.transform.position, GameObject.FindObjectOfType<Player_Controller_v1>().transform.position) <= mouseOverRange)
        {
            text.enabled = true;
            text.text = "Left-Click to Install Valve";

            if (Input.GetMouseButtonDown(0) && valveFound)
            {
                valveInstalled = true;
                waterValve.SetActive(true);
            }
        }

        if (!valveOpenAndActivated && valveInstalled && Vector3.Distance(gameObject.transform.position, GameObject.FindObjectOfType<Player_Controller_v1>().transform.position) <= mouseOverRange)
        {
            text.enabled = true;
            text.text = "Left-Click to Turn Valve";


            if (Input.GetMouseButtonDown(0))
            {
                valveOpenAndActivated = true;
                anim.SetTrigger("OpenValve");
                turnValveSFX.Play();
                LowerWaterLevel();
                waterRushingSFX.PlayDelayed(playDelay);
                text.enabled = false;
            }
        }
    }

    private void LowerWaterLevel()
    {
        foreach (var water in waterRaised)
        {
            water.SetActive(false);
        }

        if (waterLowered != null)
        {
            waterLowered.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        text.enabled = false;
    }

    public void SetValveFound(bool state)
    {
        valveFound = state;
    }

    public bool GetValveOpened()
    {
        return valveOpenAndActivated;
    }
}