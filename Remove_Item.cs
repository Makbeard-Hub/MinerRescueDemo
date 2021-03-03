using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Generalizable script to add to items that are meant to be removed
 * 
 * 
 * Script created 5/10/2020 by M. Justice
*/
public class Remove_Item : MonoBehaviour
{
    [SerializeField] GameObject removeTextMsg;
    [SerializeField] float interactionRange = 3f; 

    private void OnMouseOver()
    {
        if (!PlayerInRange())
        {
            return;
        }

        removeTextMsg.GetComponent<Text>().enabled = true;

        //If the player left-clicks the item, hide the item in the world
        if (Input.GetMouseButtonDown(0))
        {
            HideObject();
        }
    }

    private bool PlayerInRange()
    {
        if (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, gameObject.transform.position) <= interactionRange)
        {
            return true;
        }
        else { return false; }
    }

    private void OnMouseExit()
    {
        removeTextMsg.GetComponent<Text>().enabled = false;
    }

    void HideObject()
    {
        //Get and disable collider
        Collider myCollider = GetComponent<Collider>();
        myCollider.enabled = false;

        //Get and disable renderers
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        //Ensure take text is gone
        removeTextMsg.GetComponent<Text>().enabled = false;
    }
}
