using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas_Mask_Equip : MonoBehaviour
{
    [SerializeField]
    bool _personalMaskAcquired = false;

    [SerializeField]
    int _maskCount;

    GameObject[] _gasEmitters;

    private void Start()
    {
        _gasEmitters = GameObject.FindGameObjectsWithTag("Gas");
    }

    public void AddMask()
    {
        //If the player doesn't have a mask, set the bool to true
        //and equip the player
        if (!_personalMaskAcquired)
        {
            _personalMaskAcquired = true;

            //Make the gas emitters ignore the player
            foreach(GameObject emitter in _gasEmitters)
            {
                Gas_Functionality _gasScript = emitter.GetComponent<Gas_Functionality>();
                _gasScript.AddPermittedCharacter(gameObject);
            }

            //***Add code to "equip" player mask***
        }
        else
            _maskCount++;
    }

    //A function to check if the player has a mask available for an NPC prior to interaction
    public bool MaskAvailable()
    {
        return _maskCount > 0;
    }

    //A function to give an NPC a mask - called from NPC_Interaction
    //Should only be called after the above function returns true, but includes
    //a check for positive mask count just in case
    public bool GetMask()
    {
        if (_maskCount > 0)
        {
            _maskCount--;
            return true;
        }
        else
            return false;
            
    }

    public bool PlayerHasMask()
    {
        return _personalMaskAcquired;
    }
}
