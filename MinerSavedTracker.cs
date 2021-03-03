using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/* This Class is meant to track the number of miners saved throughout the game.
 * Initially displays UI when first miner is saved. Updates each miner saved thereafter.
 * Created by M. Justice 7/17/2020 
 * */
public class MinerSavedTracker : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI _text;

    private float minerSavedCount = 0f;

    // Option To Hide UI Until First NPC Saved
    private bool initiatedInGame = false;

    private void Start()
    {
        image.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
    }

    public void MinerSavedIncrease()
    {
        if (!initiatedInGame)
        {
            SetUIState();
        }
        minerSavedCount++;
        _text.SetText("Saved: " + minerSavedCount);
    }

    private void SetUIState()
    {
        initiatedInGame = true;
        image.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
    }
}