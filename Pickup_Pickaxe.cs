using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Pickup_Pickaxe : MonoBehaviour
{
    [SerializeField] float mouseOverRange = 5f;
    [SerializeField] Text text;

    //Audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pickupSFX;

    Mine_Object mine_Object;
    bool minerAssigned = false;

    private void OnMouseOver()
    {
        if (!minerAssigned)
        {
            AssignMiner();
        }

        if (Vector3.Distance(gameObject.transform.position, mine_Object.transform.position) <= mouseOverRange)
        {
            text.enabled = true;
            text.text = "Left-Click to Acquire Pickaxe";

            if (Input.GetMouseButtonDown(0))
            {
                mine_Object.SetPickaxeCollected();
                gameObject.SetActive(false);
                text.enabled = false;
            }
        }
    }

    private void AssignMiner()
    {
        mine_Object = FindObjectOfType<Mine_Object>();
        minerAssigned = true;
    }

    private void OnMouseExit()
    {
        text.enabled = false;
    }
}