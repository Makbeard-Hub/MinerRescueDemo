using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Simple script to flip the light 
 *  states for lights attached to switches.
 *  
 *  Created by A. Jayasinghe on 06/17/2020
 * 
 */

public class Switch_Light_Controller : MonoBehaviour
{
    [SerializeField]
    Material _activeMat, _deactiveMat;

    [SerializeField]
    bool _state = false;

    MeshRenderer _myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _myRenderer = GetComponent<MeshRenderer>();
    }

    public void flipState()
    {
        _state = !_state;

        if (_state == true)
            _myRenderer.material = _activeMat;
        else
            _myRenderer.material = _deactiveMat;
    }
}
