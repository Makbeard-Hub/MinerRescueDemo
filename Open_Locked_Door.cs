using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Locked_Door : MonoBehaviour
{
    //This door's associated keycard lock
    //    public GameObject cardLock;
    //    Read_Keycard _cardLockScript;

    Powered_Component _powerScript;

    bool _doorOpened = false;

    Animator _doorAnim;

    [SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip doorOpenSFX;

    // Start is called before the first frame update
    void Start()
    {
        //   _cardLockScript = cardLock.GetComponent<Read_Keycard>();
        _powerScript = GetComponent<Powered_Component>();

        _doorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Open the door when it receives power
        if (_doorOpened == false && /*_cardLockScript.IsLocked() == false*/ 
            _powerScript.GetPowerStatus() == true)
        {
            _doorAnim.SetTrigger("Open");
            audioSource.Play();
            _doorOpened = true;
        }
    }
}
