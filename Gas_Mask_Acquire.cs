using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas_Mask_Acquire : MonoBehaviour
{
    GameObject _player;
    Gas_Mask_Equip _maskEquipScript;

    GameObject _takeTextObj;
    [SerializeField]
    //The UI object called "Take Text" in the Canvas
    Text _takeText;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _maskEquipScript = _player.GetComponent<Gas_Mask_Equip>();

        _takeTextObj = GameObject.FindGameObjectWithTag("Take Text");
        _takeText = _takeTextObj.GetComponent<Text>();
    }

    private void OnMouseOver()
    {
        _takeText.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            //Give the player another mask
            _maskEquipScript.AddMask();

            //Hide the object
            gameObject.SetActive(false);

            //Hide the UI text
            _takeText.enabled = false;
        }
    }

    private void OnMouseExit()
    {
        _takeText.enabled = false;
    }
}
