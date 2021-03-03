using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle the button input for elevators
// Created by A. Jayasinghe
// 
// Replaced bools with enums
// Edited by M. Justice 5/27/2020
// Removed Call to MoveDown
// Edited by M. Justice 7/30/2020
public class Elevator_Button : MonoBehaviour
{
    ////Mutually exclusive bools to define what type of button this is
    //public bool upArrow;
    //public bool downArrow;

    [SerializeField] ElevatorDirection elevatorDirection;

    public enum ElevatorDirection
    {
        Up,
        Down
    }

    //The elevator housing this button
    public GameObject myElevator;

    //private void Start()
    //{
    //    //Error check to ensure only one of the arrow bools is toggled, and
    //    //that at LEAST one is toggled
    //    if (upArrow && downArrow)
    //    {
    //        throw new UnityException(gameObject.name +
    //            "has both arrow bools toggled on. This is a mistake. Fix it.");
    //    }
    //    else if (!upArrow && !downArrow)
    //    {
    //        throw new UnityException(gameObject.name +
    //            "has neither arrow bool toggled on. This is a mistake. Fix it.");
    //    }
    //}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject == gameObject)
                {
                    SendElevatorSignal();
                }
            }
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
