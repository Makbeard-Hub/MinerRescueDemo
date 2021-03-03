using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling power states of powered components
//Read from this script to toggle behavior of those components
//
//Created 11/22/2019 by A. Jayasinghe
// Edited Control Panel name 
// Edited 5/27/2020 by M. Justice
public class Powered_Component : MonoBehaviour
{
    //Lights indicating the state of each powered object
    public GameObject[] attachedLights;

    //The object responsible for powering this component
    public GameObject[] inputObjects;

    bool _powered = false;

    // Update is called once per frame
    void Update()
    {
        //Set the _powered boolean based on the input's status
        if (!_powered)
        {
            _powered = CheckInputs();
        }
    }

    //Method allowing a caller to set the power status of
    //this component
    public void SetPowerStatus(bool state)
    {
        _powered = state;
    }

    //Method allowing a caller to get the power status of
    //this component
    public bool GetPowerStatus()
    {
        return _powered;
    }

    //Method to change the color of the lights attached to this object
    void ChangeLights(int index, bool state)
    {
        //foreach (GameObject light in attachedLights)
        //{
        //    Light_States lightScript = light.GetComponent<Light_States>();
        //    lightScript.SetPowerStatus(state);
        //}

        //Bound check to make sure the light index exists
        if(index >= 0 && index < attachedLights.Length)
        {
            //Swap the appropriate light's power state
            Light_States lightScript = attachedLights[index].GetComponent<Light_States>();
            lightScript.SetPowerStatus(state);
            //Debug.Log("Swapping light material to " + state);

        }

    }

    //Method to check the power from whatever input is attached to this component
    //such as a switch or the control panel
    bool CheckInputs()
    {
        int poweredCount = 0;

        for(int i = 0; i < inputObjects.Length; ++i)
        {
            GameObject obj = inputObjects[i];
            switch (obj.tag)
            {
                case "Control Panel":
                    FuseBox_Puzzle panelScript = obj.GetComponent<FuseBox_Puzzle>();
                    if (panelScript.GetPowerState())
                    {
                        ChangeLights(i, true);
                        poweredCount++;
                    }
                    break;
                case "Keycard Reader":
                    Read_Keycard keycardScript = obj.GetComponent<Read_Keycard>();

                    //Flip the state of the "IsLocked" method for consistency
                    bool _keyCardReaderUnlocked = !keycardScript.IsLocked();

                    if (_keyCardReaderUnlocked)
                    {
                        ChangeLights(i, true);
                        poweredCount++;
                    }
                    break;
                case "Codelock":
                    Read_Code codeLockScript = obj.GetComponent<Read_Code>();
                    if (codeLockScript.IsUnlocked())
                    {
                        ChangeLights(i, true);
                        poweredCount++;
                    }
                    break;
                case "Power Switch":
                    Power_Switch switchScript = obj.GetComponent<Power_Switch>();
                    if (switchScript.getActiveState())
                    {
                        //Debug.Log("Switch flipped");
                        ChangeLights(i, true);
                        poweredCount++;
                    }
                    else
                    {
                        ChangeLights(i, false);
                        poweredCount--;
                    }
                    break;
                case "Generator Switch":
                    Generator_Switch genSwitchScript = obj.GetComponent<Generator_Switch>();
                    if (genSwitchScript.IsFlipped())
                    {
                        ChangeLights(i, true);
                        poweredCount++;
                    }
                    break;
                default:
                    throw new UnityException("Invalid input object attached to " + gameObject.name);
            }
        }

        return poweredCount == inputObjects.Length;
    }
}
