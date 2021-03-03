using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script to child a player to a moving object like an elevator
//
//Created on 11/13/2019 by A. Jayasinghe
// Edited on 5/27/2020 by M. Justice
public class Child_Player_to_Moving_Obj : MonoBehaviour
{
    //Transform oldParent;

    private void OnTriggerEnter(Collider other)
    {
        //If the player enters the trigger box for this object,
        //child the player to this object
        if(other.tag == "Player")
        {
            //oldParent = transform.parent;
            //transform.SetParent(null);

            other.transform.SetParent(transform);
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Unchild the player when they leave the trigger box
        if(other.tag == "Player")
        {
            other.transform.SetParent(null);
            DontDestroyOnLoad(other.gameObject);

            //transform.SetParent(oldParent);
        }
    }
}
