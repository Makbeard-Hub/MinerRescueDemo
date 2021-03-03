using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Basic movement script - subject to changes.
 *
 * Script created 11/10/2019 by A. Jayasinghe
 * Edited 12/17/2019 by M. Justice
 * Included AdjustAnimator(); scales off movement magnitude
 * Edited 3/5/2020 by M. Justice
*/
public class Player_Controller_v1 : MonoBehaviour
{

    public float moveSpeed = 1.0f;

    [SerializeField] private Animator animator;

    float _xInput = 0f;
    float _yInput = 0f;
    Rigidbody rbody;
    Vector3 movement;
    bool halfSpeed = false;

    [SerializeField] GameObject raycastOrigin;

    MovementAudioManager audioManager;

    private static Player_Controller_v1 _instance;
    public static Player_Controller_v1 Instance
        { get {return _instance;} }

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        //animator = GetComponentInChildren<Animator>();
        audioManager = GetComponent<MovementAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();

        //Move the player according to the inputs
        // transform.Translate(Vector3.right * _xInput);
        //  transform.Translate(Vector3.forward * _yInput);

        if (movement.magnitude >= 1f)
        {
            CheckGroundType();
        }

        MovePlayer();

        AdjustAnimator();
        
    }

    private void GetPlayerInput()
    {
        float newMoveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && !halfSpeed)
        {
            newMoveSpeed = moveSpeed * 3f;
        }
        else newMoveSpeed = moveSpeed;

        //Get movement axis input and modify with move speed
        _xInput = Input.GetAxis("Horizontal") * newMoveSpeed;
        _yInput = Input.GetAxis("Vertical") * newMoveSpeed;
    }

    private void MovePlayer()
    {
        movement = new Vector3(_xInput, 0, _yInput);   // (Vector3.right * _xInput + Vector3.forward * _yInput);
        movement = transform.TransformVector(movement);

        if (halfSpeed)
        {
            movement *= 0.5f;
        }

        rbody.MovePosition(transform.position + movement * Time.deltaTime);
    }

    private void AdjustAnimator()
    {
        animator.SetFloat("Speed", movement.magnitude);
    }

    private void CheckGroundType()
    {
        halfSpeed = false;

        RaycastHit hit;
        if(Physics.Raycast(raycastOrigin.transform.position, Vector3.down, out hit))
        {
            if(hit.transform.tag == "Ground")
            {
                if (movement.magnitude >= 7f)
                {
                    audioManager.PlayStoneFootsteps(0.3f);

                }
                else
                {
                    audioManager.PlayStoneFootsteps();
                }
            }
            else if(hit.transform.tag == "Puddle")
            {
                audioManager.PlayPuddleFootsteps();
            }
            else if(hit.transform.tag == "Water")
            {
                audioManager.PlayDeepWaterFootsteps();
                halfSpeed = true;
            }
            else if(hit.transform.tag == "Metal")
            {
                audioManager.PlayMetalFootsteps();
            }
        }
    }
}
