using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Class handling the player's interaction with their inventory
 * 
 * Script pulled from Space_Wurms project & modified 12/10/2019
 */
public class Inventory_Interaction : MonoBehaviour
{
    //Boolean determining if inventory is visible or not
    bool inventoryActive = false;

    //Boolean determine if an item is being moved around
    bool holdingSomething = false;

    //Item being held
    GameObject item;

    //Original position for the item
    Vector3 origPos;

    //Original parent of the item
    Transform _origParent;

    //Position of the swap item
    Vector3 swapPos;

    //Event system fields
    GraphicRaycaster gCaster;
    PointerEventData eventData;
    EventSystem eventSystem;

    GameObject _player;

    // Use this for initialization
    void Start()
    {
        //Get the raycaster from the canvas
        gCaster = GetComponent<GraphicRaycaster>();
        //Get the event system from the scene
        eventSystem = GetComponent<EventSystem>();

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Activate or deactivate the inventory screen
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            //Swap the state of the boolean
            inventoryActive = !inventoryActive;

        }

        //Check if mouse button is clicked when the inventory is visible
        //and no item is held
        if (!holdingSomething && inventoryActive &&
            Input.GetMouseButtonDown(0))
        {
            //Set up Pointer Event
            eventData = new PointerEventData(eventSystem);
            //Set pointer event position to that of the mouse
            eventData.position = Input.mousePosition;

            //Create list of raycast results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using graphics raycaster and mouse click position
            gCaster.Raycast(eventData, results);

            //Determine if one of the results is an inventory slot,
            //pick it up if true and change necessary fields
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Inventory Slot")
                {
                    //Set the bool indicating an item is being moved
                    //to true and store the item in this script's field
                    holdingSomething = true;
                    item = result.gameObject;

                    //Store the original position of this item
                    origPos = result.gameObject.transform.position;

                    //This if statement ensures this only happens once per item pickup
                    if (_origParent == null)
                    {
                        //Save the item's parent to return it after mouse release
                        _origParent = item.transform.parent;

                        //Bring the item's sprite to the front of the UI
                        item.transform.SetParent(transform);
                        item.transform.SetAsLastSibling();
                    }

                    Debug.Log("Picked up " + item.name);
                    //Break out of the loop
                    break;
                }
            }
        }

        //Check if the mouse button is held down while the inventory is active
        //and an item is held
        else if (holdingSomething && inventoryActive &&
            Input.GetMouseButton(0))
        {
            //Make the item track the mouse
            item.transform.position = Input.mousePosition;
        }

        //Check if the mouse button is released while the inventory is 
        //active and an item is held
        else if (holdingSomething && inventoryActive &&
            Input.GetMouseButtonUp(0))
        {
            //Set up Pointer Event
            eventData = new PointerEventData(eventSystem);
            //Set pointer event position to that of the mouse
            eventData.position = Input.mousePosition;

            //Create list of raycast results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using graphics raycaster and mouse click position
            gCaster.Raycast(eventData, results);

            //Determine if one of the results is an inventory slot,
            //swap it with the held item if so
            foreach (RaycastResult result in results)
            {
                //Make sure the object is looking at an inventory slot that
                //isn't the held item
                if (result.gameObject != item &&
                    result.gameObject.tag == "Hotbar Slot")
                {
                    //Store this item in the swapItem field
                    GameObject _hotbarSlot = result.gameObject;

                    //Assign the item's sprite to the hotbar slot
                    AssignHotbarSprite(item, _hotbarSlot);

                    //Break out of the loop
                    break;
                }
            }

            //Reset the object's parent
            item.transform.SetParent(_origParent);
            _origParent = null;

            //Return the now-moved item's game object to its original position
            item.transform.position = origPos;

            //Set the "held" bool to false again
            holdingSomething = false;

            //Remove the reference to the item
            item = null;
        }
    }

    /**
	 * Function to assign a sprite to a hotbar slot
	 * @param item - the item being moved
	 * @param swapItem - item in the slot the first item is being moved to
	 * @param origPos - Original position of the item being moved
	 */
    private void AssignHotbarSprite(GameObject item, GameObject hotbarSlot)
    {
        //Get the image and transform of the moving item
        Image itemImage = item.GetComponent<Image>();

        //Get the image and transform of the hotbar item
        Image hotbarImage = hotbarSlot.GetComponent<Image>();

        //Change the hotbar sprite & match its transform to that of the item
        //(to ensure appropriate scaling)
        hotbarImage.sprite = itemImage.sprite;

        //Make the hotbar icon visible
        Color hotbarColor = hotbarImage.color;
        hotbarColor.a = 1;
        hotbarImage.color = hotbarColor;

        //Update the hotbar data in the Held_Items script
        Item_Icon_Data _itemDataScript = item.GetComponent<Item_Icon_Data>();
        Hotbar_Data _hotbarDataScript = hotbarSlot.GetComponent<Hotbar_Data>();
        Held_Items _heldItemScript = _player.GetComponent<Held_Items>();

        int _itemIndex = _itemDataScript.GetInventoryIndex();
        int _hotbarIndex = _hotbarDataScript.GetSlotIndex();
        _heldItemScript.UpdateHotbar(_hotbarIndex, _itemIndex);
    }

}
