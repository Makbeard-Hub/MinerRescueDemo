using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Generalizable script to add to items that are meant to be picked up
//
//Script created 11/10/2019 by A. Jayasinghe
// Edited 5/5/2020 by M. Justice
public class Take_Item : MonoBehaviour
{

    public Text takeText;

    public int itemIndexNumber;

    GameObject player;

    Held_Items heldItemScript;

    public Image inventorySprite;
    public Image hotbarSprite;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heldItemScript = player.GetComponent<Held_Items>();
    }

    private void OnMouseOver()
    {
        takeText.GetComponent<Text>().enabled = true;

        //If the player left-clicks the item, hide the item in the world
        //and enable it in the player's inventory, visually and mechanically
        if (Input.GetMouseButtonDown(0))
        {
            heldItemScript.ToggleCollectedStatus(itemIndexNumber, true);
            HideObject();
        }
    }

    private void OnMouseExit()
    {
        takeText.GetComponent<Text>().enabled = false;
    }

    void HideObject()
    {
        //Get and disable collider
        Collider myCollider = GetComponent<Collider>();
        myCollider.enabled = false;

        //Get and disable renderers
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        //Ensure take text is gone
        takeText.GetComponent<Text>().enabled = false;
    }
}
