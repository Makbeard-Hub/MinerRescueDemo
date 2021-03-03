using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inventory management script. Current idea is to have a pre-existing
//item list, with each item given an index. When the player "gets" the item
//in the game world, the associated item index is triggered, making the item 
//appear in their inventory, and making it accessible for use.
//
//Script created 11/11/2019 by A. Jayasinghe
//
//Check for null array index value
//Edited 11/15/2019 by M. Justice
public class Inventory_Management : MonoBehaviour
{

    public GameObject inventory;

    //Array of the possible inventory keys 
    //We can modify this later if we want a different access method
    KeyCode[] _keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0
    };

    GameObject _player;
    Held_Items _helditemsScript;

    GameObject _camera;

    Mouse_Look _playerMouseScript;
    Mouse_Look _cameraMouseScript;
    Player_Controller_v1 _controlScript;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _helditemsScript = _player.GetComponent<Held_Items>();

        _camera = GameObject.FindGameObjectWithTag("MainCamera");

        _playerMouseScript = _player.GetComponent<Mouse_Look>();
        _cameraMouseScript = _camera.GetComponent<Mouse_Look>();
        _controlScript = _player.GetComponent<Player_Controller_v1>();

        if(inventory == null)
        {
            inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.SetActive(false);
        }
        
    }

    private void Update()
    {
        for(int i = 0; i < _keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(_keyCodes[i]) && _helditemsScript.items.Length > i)
            {
                TakeOutItem(i);
            }
        }

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    ToggleInventory();
        //}
    }

    //Method to enable a given item in the player's inventory
    public void AddItem(int index)
    {
        //Toggle the "collected" state to true on the given item
        _helditemsScript.ToggleCollectedStatus(index, true);

        _helditemsScript.AttachToPlayer(index);
    }

    //Method to take out a chosen item, based on index
    void TakeOutItem(int i)
    {
        //First stow all items
        _helditemsScript.DisableItems();

        //Take out desired item
        _helditemsScript.EnableItem(i);
    }

    ////Method to toggle the visual inventory on or off
    //void ToggleInventory()
    //{
    //    if (inventory.activeSelf)
    //    {
    //        inventory.SetActive(false);
    //        ToggleControls(true);

    //        //Turn off & lock the cursor
    //        Cursor.visible = false;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        inventory.SetActive(true);
    //        ToggleControls(false);

    //        //Turn on & unlock the cursor
    //        Cursor.visible = true;
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}

    //Helper method to toggle player controls
    void ToggleControls(bool state)
    {
        _playerMouseScript.enabled = state;
        _cameraMouseScript.enabled = state;
        _controlScript.enabled = state;
    }
}
