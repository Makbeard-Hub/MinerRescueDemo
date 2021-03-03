using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb_Ladder : MonoBehaviour
{
    //Relevant waypoints
    public GameObject upperWaypoint;
    public GameObject lowerWaypoint;
    public GameObject upperDetachPoint;
    public GameObject lowerDetachPoint;
    public GameObject railPoint;

    //Limiters for the rail point when the player isn't attached
    public GameObject upperRailLimiter;
    public GameObject lowerRailLimiter;

    public float moveSpeed = 3f;

    //Range the player needs to be in to click on the ladder
    public float minRange = 2f;

    GameObject _player;

    bool _playerAttached = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Distance from the player to the base of the ladder
        float _distToPlayer = Vector3.Distance(railPoint.transform.position, _player.transform.position);

        //Distance from the rail point to the upper waypoint
        float _railToUpperWP = Vector3.Distance(railPoint.transform.position, upperWaypoint.transform.position);

        //Distance from the rail point to the lower waypoint
        float _railToLowerWP = Vector3.Distance(railPoint.transform.position, lowerWaypoint.transform.position);

        //Detect the player's click on the ladder
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject && _distToPlayer <= minRange)
                {
                    AttachPlayer();
                }
            }
        }

        //Control vertical movement when player's attached
        if (_playerAttached)
        {
            _player.transform.position = railPoint.transform.position;

            //Climb to upper waypoint when 'W' is pressed
            if (Input.GetKey(KeyCode.W))
            {
                ClimbTowards(upperWaypoint);
            }
            //Climb to lower waypoint when 'S' is pressed
            else if (Input.GetKey(KeyCode.S))
            {
                ClimbTowards(lowerWaypoint);
            }

            //Detach the player depending on which waypoint they reach
            if(_railToLowerWP < .5f)
            {
                DetachPlayer(lowerDetachPoint);
            }
            else if(_railToUpperWP < .5f)
            {
                DetachPlayer(upperDetachPoint);
            }
        }

        //Move the rail point to track the player when they aren't attached
        if (!_playerAttached)
        {
            ////Track the player's Y & match the rail point's Y to it
            //float trackedY = _player.transform.position.y;

            ////Limit the rail point's Y so it doesn't leave the ladder's bounds
            //if(trackedY > upperRailLimiter.transform.position.y)
            //{
            //    trackedY = upperRailLimiter.transform.position.y;
            //}
            //else if(trackedY < lowerRailLimiter.transform.position.y)
            //{
            //    trackedY = lowerRailLimiter.transform.position.y;
            //}

            ////Set the new position
            //railPoint.transform.position = new Vector3(railPoint.transform.position.x, trackedY, railPoint.transform.position.z);

            if (_player.transform.position.y > railPoint.transform.position.y)
                railPoint.transform.position = upperRailLimiter.transform.position;
            else
                railPoint.transform.position = lowerRailLimiter.transform.position;
        }
    }

    //Method to attach the player to the ladder
    void AttachPlayer()
    {
        //Kill player's gravity & control script
        ToggleGravAndControls(false);

        //Attach the player to the rail point and match their positions
     //   _player.transform.position = railPoint.transform.position;
    //    _player.transform.parent = railPoint.transform;
        _playerAttached = true;

    }

    //Method to detach the player from the ladder
    void DetachPlayer(GameObject detachPoint)
    {
        //Unchild the player & position them
     //   _player.transform.parent = null;
        _player.transform.position = detachPoint.transform.position;
    //    _player.transform.rotation = Quaternion.identity;
        _playerAttached = false;

        //Restore gravity & controls
        ToggleGravAndControls(true);
    }

    //Helper method to climb towards a given waypoint
    void ClimbTowards(GameObject waypoint)
    {
        railPoint.transform.position = Vector3.MoveTowards(railPoint.transform.position,
                   waypoint.transform.position, moveSpeed * Time.deltaTime);
    }

    //Helper method to toggle gravity & player controls
    void ToggleGravAndControls(bool state)
    {
        Player_Controller_v1 _playerMoveScript = _player.GetComponent<Player_Controller_v1>();
        _playerMoveScript.enabled = state;
        Rigidbody _playerRB = _player.GetComponent<Rigidbody>();
        _playerRB.useGravity = state;
    }
    
}
