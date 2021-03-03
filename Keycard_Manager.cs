using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keycard_Manager : MonoBehaviour
{
    //An internal Card class, acting as a profile for each
    //card in the game
    [System.Serializable]
    public class Card
    {
        [SerializeField]
        GameObject _cardObject;

        [SerializeField]
        Sprite _cardSprite;

        [SerializeField]
        bool _acquired = false;

        public GameObject GetCardObject()
        {
            return _cardObject;
        }

        public Sprite GetCardSprite()
        {
            return _cardSprite;
        }

        public Material GetCardMat()
        {
            MeshRenderer _cardRenderer = _cardObject.GetComponent<MeshRenderer>();

            return _cardRenderer.material;
        }

        public bool IsAcquired()
        {
            return _acquired;
        }

        public void SetAcquired()
        {
            _acquired = true;
        }
    }

    [SerializeField]
    List<Card> _possibleCards;

    [SerializeField]
    Image _prevCardSlot, _curCardSlot, _nextCardSlot;

    int _curCardIndex, _prevCardIndex, _nextCardIndex = -1;

    GameObject _acquisitionTextObj;
    Text _acquisitionText;

    //Singleton
    public static Keycard_Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the current card index to the first index
        _curCardIndex = 0;

        UpdateIndeces();

        UpdateSlots(_curCardIndex, _prevCardIndex, _nextCardIndex);

        _acquisitionTextObj = GameObject.FindGameObjectWithTag("Item Acquisition Text");
        _acquisitionText = _acquisitionTextObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickNextCard();
        if (Input.GetKeyDown(KeyCode.Q))
            PickPreviousCard();
    }

    //Function to let other scripts get whatever the current card is
    //(Predominantly meant for the keycard readers)
    public GameObject GetCurrentCard()
    {
        if (_possibleCards[_curCardIndex].IsAcquired())
            return _possibleCards[_curCardIndex].GetCardObject();
        else
            return null;
    }

    //Function for acquiring new players from the world or NPCs
    public void AcquireCard(int cardIndex)
    {
        //Set the appropriate card in the list to "acquired"
        _possibleCards[cardIndex].SetAcquired();

        //Re-update the indeces & UI slots
        UpdateIndeces();
        UpdateSlots(_curCardIndex, _prevCardIndex, _nextCardIndex);

        //Tell the player what they acquired
        _acquisitionText.text = _possibleCards[cardIndex].GetCardObject().name + " acquired!";

        StartCoroutine(AcquisitionMessage());
    }

    //Function to increment the cards forward in the UI & code
    void PickNextCard()
    {
        //Placeholder just to prevent an infinite loop
        int startIndex = _curCardIndex;

        //Keeps incrementing the current index until a card the player
        //has is found in the list
        do
        {
            _curCardIndex++;
            CheckBounds(ref _curCardIndex);
        }
        while (_curCardIndex != startIndex && _possibleCards[_curCardIndex].IsAcquired() == false);

        UpdateIndeces();
        UpdateSlots(_curCardIndex, _prevCardIndex, _nextCardIndex);
    }

    //Function to decrement the cards backward in the UI & code
    void PickPreviousCard()
    {
        //Placeholder just to prevent an infinite loop
        int startIndex = _curCardIndex;

        //Keeps decrementing the current index until a card the player
        //has is found in the list
        do
        {
            _curCardIndex--;
            CheckBounds(ref _curCardIndex);
        }
        while (_curCardIndex != startIndex && _possibleCards[_curCardIndex].IsAcquired() == false);

        UpdateIndeces();
        UpdateSlots(_curCardIndex, _prevCardIndex, _nextCardIndex);
    }

    //Function to update the current, next, and previous card indeces
    void UpdateIndeces()
    {
        _prevCardIndex = _curCardIndex - 1;
        _nextCardIndex = _curCardIndex + 1;

        CheckBounds(ref _nextCardIndex);
        CheckBounds(ref _prevCardIndex);

        //Iterate from the index immediately prior to the current index until
        //an acquired card's index is found - if one isn't found, the "previous card" index
        //will loop around and end up equal to the current card index.
        while (_prevCardIndex != _curCardIndex)
        {
            if (_possibleCards[_prevCardIndex].IsAcquired() == false)
            {
                _prevCardIndex--;
                CheckBounds(ref _prevCardIndex);
            }
            else
                break;
        }

        //Iterate from the index immediately following to the current index until
        //an acquired card's index is found - if one isn't found, the "next card" index
        //will loop around and end up equal to the current card index.
        while (_nextCardIndex != _curCardIndex)
        {
            if (_possibleCards[_nextCardIndex].IsAcquired() == false)
            {
                _nextCardIndex++;
                CheckBounds(ref _nextCardIndex);
            }
            else
                break;
        }
    }

    //Function the update the UI slots. Uses the indeces found via the UpdateIndeces
    //function to appropriately update the sprites of the UI keycard slots. If no appropriate
    //index exists for a given slot, it's left "blank."
    void UpdateSlots(int curIndex, int prevIndex, int nextIndex)
    {

        if (_possibleCards[curIndex].IsAcquired())
        {
            _curCardSlot.sprite = _possibleCards[curIndex].GetCardSprite();
            Color _curCardColor = _curCardSlot.color;
            _curCardColor.a = 1;
            _curCardSlot.color = _curCardColor;
        }
        else
        {
            _curCardSlot.sprite = null;
            Color _curCardColor = _curCardSlot.color;
            _curCardColor.a = 0;
            _curCardSlot.color = _curCardColor;
        }

        if (prevIndex != curIndex)
        {
            _prevCardSlot.sprite = _possibleCards[prevIndex].GetCardSprite();
            Color _prevCardColor = _prevCardSlot.color;
            _prevCardColor.a = 1;
            _prevCardSlot.color = _prevCardColor;
        }
        else
        {
            _prevCardSlot.sprite = null;
            Color _prevCardColor = _prevCardSlot.color;
            _prevCardColor.a = 0;
            _prevCardSlot.color = _prevCardColor;
        }

        if (nextIndex != curIndex && nextIndex != prevIndex)
        {
            _nextCardSlot.sprite = _possibleCards[nextIndex].GetCardSprite();
            Color _nextCardColor = _nextCardSlot.color;
            _nextCardColor.a = 1;
            _nextCardSlot.color = _nextCardColor;
        }
        else
        {
            _nextCardSlot.sprite = null;
            Color _nextCardColor = _nextCardSlot.color;
            _nextCardColor.a = 0;
            _nextCardSlot.color = _nextCardColor;
        }


    }

    //Simple bound-checking function so indeces don't go out of bounds
    void CheckBounds(ref int index)
    {
        if (index > _possibleCards.Count - 1)
            index = 0;
        else if (index < 0)
            index = _possibleCards.Count - 1;
    }

    IEnumerator AcquisitionMessage()
    {
        _acquisitionText.enabled = true;

        yield return new WaitForSeconds(5);

        _acquisitionText.enabled = false;
    }
}
