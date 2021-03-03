using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickupValve : MonoBehaviour
{
    [SerializeField] Water_Controls controls;
    [SerializeField] float mouseOverRange = 5f;
    [SerializeField] Text text;

    //Audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pickupSFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.FindObjectOfType<Player_Controller_v1>().transform.position) <= mouseOverRange)
        {
            text.enabled = true;
            text.text = "Left-Click to Pickup Valve";

            if (Input.GetMouseButtonDown(0))
            {
                controls.SetValveFound(true);
                gameObject.SetActive(false);
                text.enabled = false;
            }
        }
    }

    private void OnMouseExit()
    {
        text.enabled = false;
    }

}
