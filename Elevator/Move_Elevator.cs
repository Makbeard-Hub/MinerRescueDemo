using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to move the elevators
//Currently moves them via script - might want to consider using animations later
//Created 11/13/2019 by A. Jayasinghe
//
// Modified code to check Powered_Component.cs for Power State. Reduced distance variation allowed from 0.5f to 0.01f.
// Edited 5/27/2020 by M. Justice
// Removed external waypoints. Now elevator moves Up only by a set distance.
// Edited 7/30/2020 by M. Justice
public class Move_Elevator : MonoBehaviour
{

    ////Publically modifiable floor limits
    ////depending on where we want the elevator to stop
    ////Uses 0 as the lowest index, since the waypoints are in an array
    //public int topPossibleFloorIndex = 4;
    //public int bottomPossibleFloorIndex = 0;

    ////Array of waypoints the elevator can move to
    //public GameObject[] elevatorWaypoints;
    //int _currentFloorIndex = 0;

    [SerializeField] private float distanceToTravelUpwards = 20f;

    private Vector3 elevatorWaypoint;

    [SerializeField] bool isOpen;
    Animator anim;

    [SerializeField] private float moveSpeed;

    bool _moving = false;
    bool _moveUp = false;
    bool _moveDown = false;

    //Vector3 _nextWaypoint;
    float distToWaypoint = 0f;

    //Audio
    [SerializeField] AudioSource gateMoveOpenSFX;
    [SerializeField] AudioSource gateMoveCloseSFX;
    [SerializeField] AudioSource approvedElevatorMoveButtonSFX;
    [SerializeField] AudioSource deniedElevatorMoveButtonSFX;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Animate();
        gateMoveOpenSFX.Play();

        elevatorWaypoint = transform.position + transform.up * distanceToTravelUpwards;
        distToWaypoint = Vector3.Distance(transform.position, elevatorWaypoint);
    }

    private void Update()
    {
        if (elevatorWaypoint != Vector3.zero)
        {
            distToWaypoint = Vector3.Distance(transform.position, elevatorWaypoint);
        }

        if(_moveUp == true && distToWaypoint > 0.01f)
        {
            _moving = true;

            //Move the elevator to the next waypoint
            transform.position = Vector3.MoveTowards(transform.position, elevatorWaypoint, moveSpeed * Time.deltaTime);
        }

        //else if(_moveDown == true && distToWaypoint > 0.01f)
        //{
        //    _moving = true;

        //    //Move the elevator to the next waypoint
        //    transform.position = Vector3.MoveTowards(transform.position, elevatorWaypoint, moveSpeed * Time.deltaTime);
        //}

        if(_moveUp == true && distToWaypoint <= 0.01f)
        {
            //Use the elevator's position as a placeholder for the next waypoint
            //until another is received
            elevatorWaypoint = transform.position;
            _moving = false;
            _moveUp = false;

            //open gates
            isOpen = true;
            Animate();
            gateMoveOpenSFX.Play();
        }
    }

    public void SetWaypoint(Vector3 newWaypoint)
    {
        elevatorWaypoint = newWaypoint;
    }

    private void Animate()
    {
        anim.SetBool("isOpen", isOpen);
    }

    public void MoveUp()
    {
        //if(GetComponent<Powered_Component>().GetPowerStatus() == false)
        //{
        //    return;
        //}

        if(GetComponent<Elevator_Permissions>().GetPermissionToMove() == false)
        {
            Debug.Log("There is no power to this device.");
            deniedElevatorMoveButtonSFX.Play();
            return;
        }

        if(!_moving)
        {
            ////Get the next waypoint's vector coords
            //_nextWaypoint = elevatorWaypoints[_currentFloorIndex + 1].transform.position;

            ////Increment floor index
            //_currentFloorIndex++;

            //Trigger the bool to move up
            _moveUp = true;

            approvedElevatorMoveButtonSFX.Play();

            //Close the gates
            isOpen = false;
            Animate();
            gateMoveCloseSFX.Play();

            SetWaypoint(transform.position + transform.up * distanceToTravelUpwards);

            RemoveOtherElevators();
        }
    }

    //public void MoveDown()
    //{
    //    //if (GetComponent<Powered_Component>().GetPowerStatus() == false)
    //    //{
    //    //    return;
    //    //}

    //    if (GetComponent<Elevator_Permissions>().GetPermissionToMove() == false)
    //    {
    //        Debug.Log("There is no power to this device.");
    //        return;
    //    }

    //    if (!_moving && _currentFloorIndex > bottomPossibleFloorIndex)
    //    {
    //        //Get the next waypoint's vector coords
    //        _nextWaypoint = elevatorWaypoints[_currentFloorIndex - 1].transform.position;

    //        //Decrement floor index
    //        _currentFloorIndex--;

    //        //Trigger the bool to move down
    //        _moveDown = true;

    //        //Close the gates
    //        isOpen = false;
    //        Animate();

    //        RemoveOtherElevators();
    //    }
    //}

    private void RemoveOtherElevators()
    {
        Move_Elevator[] elevators = GameObject.FindObjectsOfType<Move_Elevator>();
        foreach (Move_Elevator elev in elevators)
        {
            if (elev == this) continue;

            elev.gameObject.SetActive(false);
        }
    }
}
