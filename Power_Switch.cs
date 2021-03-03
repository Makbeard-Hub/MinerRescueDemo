using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Switch : MonoBehaviour
{

    //Range the player needs to be in to click on the ladder
    public float minRange = 2f;

    GameObject _player;

    Animator _switchAnimator;

    bool _activated = false;

    [SerializeField]
    GameObject[] _attachedLights;

    //Audio
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _switchAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance from the player to the base of the ladder
        float _distToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        //Detect the player's click on the ladder
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject && _distToPlayer <= minRange)
                {
                    //Trigger the appropriate animation and state change
                    //based on the switch's original state
                    if (_activated)
                        FlipUp();
                    else
                        FlipDown();

                    //Flip the states of attached lights
                    foreach(GameObject obj in _attachedLights)
                    {
                        Switch_Light_Controller _lightScript = 
                            obj.GetComponent<Switch_Light_Controller>();

                        _lightScript.flipState();
                    }
                }
            }
        }
    }

    public bool getActiveState()
    {
        return _activated;
    }

    //void CompressButton()
    //{
    //    _buttonAnimator.SetTrigger("Compress");
    //    _activated = true;
    //}

    void FlipUp()
    {
        _switchAnimator.SetTrigger("Up");
        _activated = false;

        audioSource.Play();
    }

    void FlipDown()
    {
        _switchAnimator.SetTrigger("Down");
        _activated = true;

        audioSource.Play();
    }
}
