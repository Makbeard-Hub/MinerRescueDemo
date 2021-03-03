using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keycard_Pickup : MonoBehaviour
{
    GameObject _takeTextObj;
    Text _takeText;

    [SerializeField]
    int _cardIndex;

    GameObject _keycardManager;
    Keycard_Manager _managerScript;

    private void Start()
    {
        _takeTextObj = GameObject.FindGameObjectWithTag("Take Text");
        _takeText = _takeTextObj.GetComponent<Text>();

        _keycardManager = GameObject.FindGameObjectWithTag("Keycard Manager");
        _managerScript = _keycardManager.GetComponent<Keycard_Manager>();
    }

    private void OnMouseOver()
    {
        _takeText.GetComponent<Text>().enabled = true;

        //If the player left-clicks the card, hide the card in the world
        //and enable it in the player's inventory, visually and mechanically
        if (Input.GetMouseButtonDown(0))
        {
            _managerScript.AcquireCard(_cardIndex);
            HideObject();
        }
    }

    private void OnMouseExit()
    {
        _takeText.GetComponent<Text>().enabled = false;
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
        _takeText.GetComponent<Text>().enabled = false;
    }
}
