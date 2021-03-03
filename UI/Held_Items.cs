using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Simple script handling activation and deactivation of items held by the player
 * Created by A. Jayasinghe on 11/11/2019
 * Added AttachToPlayer() which will move the move the designated held item to an assigned transform and set it as its parent.
 * Edited by M. Justice 3/5/2020
 * Edited by A. Jayasinghe on 5/29/2020
*/
public class Held_Items : MonoBehaviour
{
    [System.Serializable]
    //Nested item class defining data specific to each item
    public class Item
    {
        public GameObject itemObj = null;
        public Image itemInventoryImage;
        public bool _collected = false; //**Set to private later**
        public Transform playersHoldingLocation;

        ////Toggle renderers on the item's game object
        //public void ToggleRenderers(bool state)
        //{
        //    MeshRenderer[] renderers = itemObj.GetComponentsInChildren<MeshRenderer>();
        //    foreach (MeshRenderer renderer in renderers)
        //    {
        //        renderer.enabled = state;
        //    }
        //}

        ////Toggle scripts on the item's game object
        //public void ToggleScripts(bool state)
        //{
        //    MonoBehaviour[] scripts = itemObj.GetComponents<MonoBehaviour>();
        //    foreach (MonoBehaviour script in scripts)
        //    {
        //        script.enabled = state;
        //    }
        //}

        //Toggle object on or off
        public void ToggleObject(bool state)
        {
            itemObj.SetActive(state);
        }

        //Method to toggle the collected property & activate the inventory sprite
        public void ToggleCollectedStatus(bool state)
        {
            _collected = state;
            itemInventoryImage.GetComponent<Image>().enabled = true;
        }

        //Method telling caller if this item has been "collected" or not
        public bool IsCollected()
        {
            return _collected;
        }

        public void AttachToPlayer()
        {
            if (playersHoldingLocation != null)
            {
                itemObj.transform.position = playersHoldingLocation.position;
                itemObj.transform.rotation = playersHoldingLocation.rotation;
                itemObj.transform.parent = playersHoldingLocation;
            }
        }
    }
    

    [TextArea]
    public string itemNotes;

    //Array of Item objects
    public Item[] items;

    //Array of hotbar objects
    Item[] _hotbarItems = new Item[4];

    Item _heldItem;

    //Method to make an item visible and enable its scripts
    public void EnableItem(int index)
    {
    //    Debug.Log("Enabling " + items[index]);

        //Get the item from the array
        if(items[index] != null)
        {
            Item selectedItem = items[index];

            _heldItem = selectedItem;

            if (selectedItem.IsCollected())
            {
                selectedItem.ToggleObject(true);

                ////Enable the item's renderers
                //selectedItem.ToggleRenderers(true);

                ////Enable the item's scripts
                //selectedItem.ToggleScripts(true);
            }

            if(selectedItem.itemObj.tag == "Notepad")
            {
                Notepad_Manager _notepadScript = selectedItem.itemObj.GetComponent<Notepad_Manager>();
                _notepadScript.ToggleCanvas(true);
            }
        } 
    }

    //Method to visually & scriptually (is this a word?) disable all items
    public void DisableItems()
    {
        _heldItem = null;

        foreach (Item item in items)
        {
            if(item.IsCollected())
            {
                ////Disable all renderers
                //item.ToggleRenderers(false);

                //item.ToggleScripts(false);

                //Disable all objects
                item.ToggleObject(false);

                if (item.itemObj.tag == "Notepad")
                {
                    Notepad_Manager _notepadScript = item.itemObj.GetComponent<Notepad_Manager>();
                    _notepadScript.ToggleCanvas(false);
                }
            }

        }
    }

    //Method to toggle the "collected" property of a given item
    public void ToggleCollectedStatus(int index, bool state)
    {
        Item item = items[index];
        item.ToggleCollectedStatus(state);
    }

    public void AttachToPlayer(int index)
    {
        Item item = items[index];

        item.AttachToPlayer();
    }

    //Method to get the item the player is holding
    public GameObject GetHeldItem()
    {
        return _heldItem.itemObj;
    }

    //Method to update the hotbar using the index of the item
    //from the full item array - requires knowing which item has which index
    public void UpdateHotbar(int hotbarIndex, int itemIndex)
    {
        _hotbarItems[hotbarIndex] = items[itemIndex];
    }

}
