using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Open_Generator_Switch_Cover : MonoBehaviour
{
    Powered_Component _powerScript;
    Animator _coverAnim;
    bool _opened = false;

    // Start is called before the first frame update
    void Start()
    {
        _powerScript = GetComponent<Powered_Component>();
        _coverAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_powerScript.GetPowerStatus() == true && _opened == false)
        {
            _opened = true;

            _coverAnim.SetTrigger("Open Cover");
        }
    }

    public bool IsOpen()
    {
        return _opened;
    }
}
