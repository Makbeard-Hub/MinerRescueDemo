using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate_Steam_Barrier : MonoBehaviour
{
    Water_Controls _waterControlScript;

    // Start is called before the first frame update
    void Start()
    {
        _waterControlScript = GetAttachedControlScript();

        if (_waterControlScript == null)
            throw new UnityException("Problem finding water control script in " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(_waterControlScript.GetValveOpened() == true)
        {
            StartCoroutine(DelayedSteamHalt());
        }
    }

    IEnumerator DelayedSteamHalt()
    {
        yield return new WaitForSeconds(1);

        //Kill the collider
        BoxCollider _steamBarrierColl = GetComponentInChildren<BoxCollider>();
        _steamBarrierColl.enabled = false;

        //Kill the steam particles
        ParticleSystem _steamParticles = GetComponentInChildren<ParticleSystem>();
        _steamParticles.Stop();
    }

    Water_Controls GetAttachedControlScript()
    {
        Transform parent = transform.parent;

        for(int i = 0; i < parent.childCount; ++i)
        {
            if(parent.GetChild(i).gameObject.tag == "Steam Valve Pipe")
            {
                Water_Controls _controlScript = parent.GetChild(i).gameObject.GetComponent<Water_Controls>();
                return _controlScript;
            }
        }

        return null;
    }
}
