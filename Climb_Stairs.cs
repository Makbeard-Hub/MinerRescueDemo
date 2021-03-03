using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb_Stairs : MonoBehaviour
{
    GameObject player;
    Rigidbody playerRB;

    public GameObject heightReader;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(PlayerAbove() == false && other.gameObject.tag == "Player")
        {
            playerRB.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            playerRB.useGravity = true;
        }
    }

    bool PlayerAbove()
    {
        float playerY = player.gameObject.transform.position.y;
        float heightReaderY = heightReader.transform.position.y;

        if(playerY > heightReaderY)
        {
            return true;
        }
        else{
            return false;
        }
    }
}
