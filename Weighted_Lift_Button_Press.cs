using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted_Lift_Button_Press : MonoBehaviour
{

    [SerializeField]
    GameObject _attachedLift;
    Weighted_Lift_Actuation _actuationScript;

    [TextArea]
    public string tooltipText;

    [SerializeField]
    bool _moveDirection;

    GameObject _player;
    float _distToPlayer = 0f;

    private void Start()
    {
        _actuationScript = _attachedLift.GetComponent<Weighted_Lift_Actuation>();

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _distToPlayer = Vector3.Distance(transform.position, _player.transform.position);
    }

    private void OnMouseOver()
    {

        //If the player left-clicks the item, hide the item in the world
        //and enable it in the player's inventory, visually and mechanically
        if (Input.GetMouseButtonDown(0) && _distToPlayer < 3f)
        {
            Debug.Log("Button call to move");
            _actuationScript.Move(_moveDirection);
        }

    }
}
