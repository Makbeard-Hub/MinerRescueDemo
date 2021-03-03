using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notepad_Manager : MonoBehaviour
{
    //The canvas holding the code text objects
    [SerializeField]
    GameObject _codeCanvas;

    [SerializeField] bool notePadCollected = true;
    [SerializeField] Image notepadIcon;

    //UI text objects for codes
    public Text[] codes;

    GameObject _notepad;

    Held_Items _itemScript;

    GameObject _acquisitionTextObj;
    Text _acquisitionText;

    // Start is called before the first frame update
    void Start()
    {
        //_codeCanvas = GetComponentInChildren<Canvas>();
        _codeCanvas.SetActive(false);

        if (notePadCollected)
            notepadIcon.enabled = true;
        else 
            notepadIcon.enabled = false;

        _acquisitionTextObj = GameObject.FindGameObjectWithTag("Item Acquisition Text");
        _acquisitionText = _acquisitionTextObj.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) /*&& notePadCollected*/)
        {
            ToggleCanvas(!_codeCanvas.activeSelf);
        }
    }

    public void ToggleCanvas(bool state)
    {
        _codeCanvas.SetActive(state);
    }

    public void CollectedNotepad()
    {
        notepadIcon.enabled = true;
        notePadCollected = true;
    }

    public void AddCode(int index, int newCode)
    {
        if (index < 0 && index > codes.Length - 1)
            throw new UnityException("Code index out of range");

        codes[index].text = newCode.ToString();

        //Tell the player what they acquired
        _acquisitionText.text = "New code added to notepad.";

        StartCoroutine(AcquisitionMessage());
    }

    IEnumerator AcquisitionMessage()
    {
        _acquisitionText.enabled = true;

        yield return new WaitForSeconds(5);

        _acquisitionText.enabled = false;
    }
}
