using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Read_Keycard : MonoBehaviour
{

    GameObject _cardTextObj;
    Text _cardText;

    GameObject _player;
    //Held_Items heldItemsScript;

    bool _reading = false;
    bool _locked = true;

    public Material rejectionMat;
    public Material acceptedMat;
    public Material requiredMat;

    public GameObject childCard;
    MeshRenderer cardRenderer;

    public GameObject display;
    MeshRenderer displayRenderer;

    GameObject _keycardManager;
    Keycard_Manager _managerScript;

    [SerializeField] float interactionRange = 3f;

    //Audio
    [SerializeField] AudioSource correctKeycardSFX;
    [SerializeField] AudioSource wrongKeycardSFX;

    // Start is called before the first frame update
    void Start()
    {
        _cardTextObj = GameObject.FindGameObjectWithTag("Card Text");
        _cardText = _cardTextObj.GetComponent<Text>();

        _player = GameObject.FindGameObjectWithTag("Player");
    //    heldItemsScript = _player.GetComponent<Held_Items>();

        //Get appropriate renderers
        cardRenderer = childCard.GetComponent<MeshRenderer>();
        cardRenderer.enabled = false;
        displayRenderer = display.GetComponent<MeshRenderer>();

        _keycardManager = GameObject.FindGameObjectWithTag("Keycard Manager");
        _managerScript = _keycardManager.GetComponent<Keycard_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool PlayerInRange()
    {
        if (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, gameObject.transform.position) <= interactionRange)
        {
            return true;
        }
        else { return false; }
    }

    private void OnMouseOver()
    {
        if (!PlayerInRange())
        {
            return;
        }

        _cardText.GetComponent<Text>().enabled = true;

        GameObject _curCard = _managerScript.GetCurrentCard();

        if(_curCard != null && !_reading && Input.GetMouseButtonDown(0))
        {
            Read_Card(_curCard);
        }

        //GameObject heldObj = heldItemsScript.GetHeldItem();

        ////If the player's holding a keycard and clicks on the reader,
        ////have it read the card
        //if (heldObj != null && heldObj.tag == "Keycard" && !_reading && Input.GetMouseButtonDown(0))
        //{
        //    Read_Card(heldObj);
        //}
    }

    private void OnMouseExit()
    {
        //Remove text when the cursor leaves the object
        _cardText.GetComponent<Text>().enabled = false;
    }

    //Method to read the card, modifying visuals and behavior as required
    void Read_Card(GameObject card)
    {
        //Toggle the read delay
        _reading = true;
        StartCoroutine(VisualDelays(card));

        //Check the player's card material against the required material
        bool isValidCard = CheckCardMat(card);
        Debug.Log("Valid card = " + isValidCard);
        StartCoroutine(ReadStatus(isValidCard));
    }

    //Method to compare the player card material with the required material
    bool CheckCardMat(GameObject card)
    {
        MeshRenderer playerCardRend = card.GetComponent<MeshRenderer>();
        Material cardMat = playerCardRend.sharedMaterial;

        //Checks that the card material contains the required material name
        //(Reason for this is that the card material is always an "instance," even
        //using the sharedMaterial property. Unsure how to fix this yet, but this is
        //a usable workaround)
        if(cardMat.name.Contains(requiredMat.name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Method to tell a caller if this device is still locked
    public bool IsLocked()
    {
        return _locked;
    }

    //Enumerator to delay the reading process for visuals
    IEnumerator VisualDelays(GameObject playerCard)
    {
        //Trigger the animation
        Animator cardAnim = childCard.GetComponent<Animator>();
        cardAnim.SetTrigger("Read");

        //Get the player's card renderer
        //    MeshRenderer playerCardRenderer = playerCard.GetComponent<MeshRenderer>();

        //Get the material of the current card
        MeshRenderer _cardRend = playerCard.GetComponent<MeshRenderer>();
        Material _cardMat = _cardRend.sharedMaterial;

        //Set the reader's card to match the player's in color
        cardRenderer.material = _cardMat;

        //Show the reader's card, hide the player's
        cardRenderer.enabled = true;
    //    playerCardRenderer.enabled = false;

        yield return new WaitForSeconds(3);

        //Show the player's card, hide the reader's
        cardRenderer.enabled = false;
    //    playerCardRenderer.enabled = true;

        //Reset the bool so the reader can work again
        _reading = false;
    }

    //Enumerator to time reader display changes
    IEnumerator ReadStatus(bool state)
    {

        yield return new WaitForSeconds(1);

        //Display red or green status on the reader's display
        //depending on if the card was valid or not
        if(state == true)
        {
            displayRenderer.material = acceptedMat;

            correctKeycardSFX.Play();

            //Unlock the reader
            _locked = false;
        }
        else
        {
            displayRenderer.material = rejectionMat;

            wrongKeycardSFX.Play();
        }

        yield return new WaitForSeconds(1);

        //Return to the default display after 1 second
        displayRenderer.material = requiredMat;
    }
}
