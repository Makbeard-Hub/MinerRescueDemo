using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas_Functionality : MonoBehaviour
{
    List<GameObject> _permittedCharacters;

    private void Start()
    {
        _permittedCharacters = new List<GameObject>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (_permittedCharacters.Contains(other.gameObject) == false)
        {
            //Harm character
            Health _healthScript = other.gameObject.GetComponent<Health>();

            if(_healthScript != null && _healthScript.IsDead() == false)
                _healthScript.ReduceHealth(20);
        }

    }

    public void AddPermittedCharacter(GameObject characterObj)
    {
        _permittedCharacters.Add(characterObj);
    }
}
