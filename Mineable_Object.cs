using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* Script created by A. Jayasinghe
 * Commented out old code. Didnt change much, PickAxe.cs talks to this script attached to mineable objects in the game.
 * Edited by M. Justice 3/6/2020
 * Edited by M. Justice 5/10/2020
 */
public class Mineable_Object : MonoBehaviour
{
    public int hitPoints = 10;

    public GameObject popupCanvas;
    TextMeshProUGUI textMesh;
    [SerializeField] float textFloatSpeed = 1f;

    private void Start()
    {
        if (popupCanvas != null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
    }


    //GameObject _pickaxe;

    //Mine_Object mineScript;

    /*
    private void Start()
    {
        _pickaxe = GameObject.FindGameObjectWithTag("Pickaxe");
        mineScript = _pickaxe.GetComponent<Mine_Object>();
    }
    */
    /*
    private void OnMouseEnter()
    {
        mineScript.SetValidTarget(true, gameObject);
    }

    private void OnMouseExit()
    {
        mineScript.ClearTarget();
    }
    */

    private void Update()
    {
        if(hitPoints <= 0)
        {
            Mine_Object mine_Object = FindObjectOfType<Mine_Object>();
            mine_Object.SetSwingState(false);

            Destroy(gameObject);
        }
    }

    public void LoseHP()
    {
        hitPoints--;
        DisplayPopupText();
    }

    private void DisplayPopupText()
    {
        if (popupCanvas == null) return;

        GameObject newText = Instantiate(popupCanvas, transform.position, Quaternion.identity);
        DamagePopup damagePopup = newText.GetComponent<DamagePopup>();
        damagePopup.Setup("-1");

        
    }

}
