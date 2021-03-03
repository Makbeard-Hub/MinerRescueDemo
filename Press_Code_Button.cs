using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle each code button's press function
//
//Created on 11/14/2019 by A. Jayasinghe
public class Press_Code_Button : MonoBehaviour
{
    //This button's digit
    public char digit;

    //Range the player needs to be in to click on the buttons
    public float minRange = 2f;

    GameObject _player;

    GameObject _parentCodeLock;
    Read_Code _codeScript;

    AudioSource _buttonPressSFX;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        //Get the code lock and its script
        _parentCodeLock = transform.parent.gameObject;
        _codeScript = _parentCodeLock.GetComponent<Read_Code>();

        _buttonPressSFX = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance from the player to the code button
        float _distToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        //Detect the player's click on the button
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject && _distToPlayer <= minRange)
                {
                    PressButton();
                }
            }
        }
    }

    //Method to "press" the button & send this script's digit to the codelock
    void PressButton()
    {
        _codeScript.AddDigit(digit);
        _buttonPressSFX.Play();
    }
}
