using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to handle moving elevator when entering a scene
// Created by M. Justice 5/28/2020
// Removed Call to MoveDown
// Edited by M. Justice 7/30/2020
public class TriggerElevator : MonoBehaviour
{
    [SerializeField] ElevatorDirection elevatorDirection;

    public enum ElevatorDirection
    {
        Up,
        Down
    }

    //The elevator housing this button
    [SerializeField] GameObject myElevator;

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.transform.gameObject == gameObject)
        //        {
        //            SendElevatorSignal();
        //        }
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SendElevatorSignal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            this.enabled = false;
        }
    }

    void SendElevatorSignal()
    {
        Move_Elevator elevatorScript = myElevator.GetComponent<Move_Elevator>();

        //Move the elevator depending on which type of arrow this is
        if (elevatorDirection == ElevatorDirection.Up)
        {
            elevatorScript.MoveUp();
        }
    }
}
