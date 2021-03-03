using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script for reading codes on code locks
//
//Created on 11/14/2019 by A. Jayasinghe
public class Read_Code : MonoBehaviour
{
    //This lock's required code (input by dev)
    public string requiredCode;

    string _inputCode = "";

    public Text displayText;

    bool _resetting;
    bool _unlocked;

    public GameObject display;
    MeshRenderer _displayRenderer;

    public Material baseMat;
    public Material rejectionMat;
    public Material acceptedMat;

    public GameObject attachedDoor;
    Animator _doorAnim;

    //Audio
    //Audio
    [SerializeField] AudioSource correctCodeSFX;
    [SerializeField] AudioSource wrongCodeSFX;
    //[SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip correctCodeSFX;
    //[SerializeField] AudioClip wrongCodeSFX;

    // Start is called before the first frame update
    void Start()
    {
        _displayRenderer = display.GetComponent<MeshRenderer>();

        _doorAnim = attachedDoor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_resetting == false && _unlocked == false && 
            _inputCode.Length == requiredCode.Length)
        {
            CheckCode();
        }
    }

    void CheckCode()
    {
        if(_inputCode == requiredCode)
        {
            //Display green screen
        //    _doorAnim.SetTrigger("Open");
            _displayRenderer.material = acceptedMat;
            _unlocked = true;
            correctCodeSFX.Play();
        }
        else
        {
            _resetting = true;

            wrongCodeSFX.Play();

            StartCoroutine(ResetLock());

        }
    }

    //Method to add a new digit to the input code and display
    public void AddDigit(char newChar)
    {
        if (!_resetting && !_unlocked)
        {
            _inputCode += newChar;
            displayText.text += newChar + " ";
        }

    }

    public bool IsUnlocked()
    {
        return _unlocked;
    }

    IEnumerator ResetLock()
    {
        wrongCodeSFX.Play();

        //Display red screen
        _displayRenderer.material = rejectionMat;

        yield return new WaitForSeconds(2);

        //Clear input code
        _inputCode = "";
        displayText.text = "";

        //Reset bool
        _resetting = false;

        //Return to regular display
        _displayRenderer.material = baseMat;
    }
}
