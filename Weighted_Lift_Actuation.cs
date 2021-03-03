using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted_Lift_Actuation : MonoBehaviour
{

    [SerializeField]
    GameObject _topWP, _bottomWP, _nextWP;

    [SerializeField]
    float _moveSpeed;

    bool _moving = false;

    float _distToWaypoint = 0f;

    Weighted_Lift_Weight_Detection _weightScript;
    bool _weightCleared = false;

    // Start is called before the first frame update
    void Start()
    {
        _nextWP = _topWP;

        _weightScript = GetComponent<Weighted_Lift_Weight_Detection>();
    }

    // Update is called once per frame
    private void Update()
    {
        _weightCleared = _weightScript.getClearedStatus();

        //Get distance to the next waypoint, presuming there is one
        if (_nextWP != null)
            _distToWaypoint = Vector3.Distance(transform.position, _nextWP.transform.position);

        if (_weightCleared && _moving == true && _distToWaypoint > 0.01f)
            //Move the elevator to the next waypoint
            transform.position = Vector3.MoveTowards(transform.position, _nextWP.transform.position, _moveSpeed * Time.deltaTime);
        else
            _moving = false;

        if (_distToWaypoint <= 0.01f)
            _moving = false;
    }

    //Method to move the lift
    //false = down; true = up
    public void Move(bool direction) {
        if (direction == false)
            _nextWP = _bottomWP;
        else
            _nextWP = _topWP;
        Debug.Log("Moving");
        _moving = true;
    }
}
