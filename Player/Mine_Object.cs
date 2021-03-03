using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script created by A. Jayasinghe
 * Commented out old code. Simplified mining by removing raycasts and reference to objects being mined.
 * Now This script controls animation of swinging a pickaxe, PickAxe.cs with a trigger to detect Mineable Objects and changes canGather in this script.
 * This determines if player can gather and deplete, but does not stop the animation.
 * Edited by M. Justice 3/6/2020
 * Reset isMining bool by using a Coroutine and the clips length
 * Edited by M. Justice 7/14/2020
 */
public class Mine_Object : MonoBehaviour
{
    [SerializeField] Image pickaxeInventorySlot;
    [SerializeField] Image pickaxeImage;
    [SerializeField] private bool pickaxeCollected = false;
    [SerializeField] Color canUseColor;

    [SerializeField] GameObject miniPickaxe;
    [SerializeField] Transform[] shoulderPickaxeSpawns;

    [SerializeField] float miningSpeed;

    GameObject myCamera;

    float _resetTimer = 0f;
    [SerializeField]
    float timeBtwnMining = 1f;

    private bool _canGather = true;
    private bool canSwing = false;

    private int spawnIndex = 0;

    private void Start()
    {
        myCamera = Camera.main.gameObject;
        pickaxeImage.enabled = false;
    }

    void Update()
    {
        //Changes UI Color if you can use the pickaxe
        if (GetSwingState() && pickaxeCollected)
        {
            pickaxeInventorySlot.color = canUseColor;
        }
        else
        {
            pickaxeInventorySlot.color = Color.white;
        }

        //Spawn pick if collected, gather timer is ok, aiming at a mineable object, and pressing button.
        if (_canGather && Input.GetButtonDown("Fire1") && GetSwingState() && pickaxeCollected)
        {
            GeneratePickaxe();
            _canGather = false;
        }

        //Increment timer when the player has mined something
        //and the timer needs to reset
        if (!_canGather && _resetTimer < timeBtwnMining)
        {
            _resetTimer += Time.deltaTime;
        }

        //When the timer exceeds max time, reset it and the bool
        if (_resetTimer >= timeBtwnMining)
        {
            _resetTimer = 0;
            _canGather = true;
        }
    }

    private void GeneratePickaxe()
    {
       GameObject currentPick = Instantiate(miniPickaxe, shoulderPickaxeSpawns[spawnIndex].position, shoulderPickaxeSpawns[spawnIndex].rotation);

        if (spawnIndex == 0)
        {
            spawnIndex = 1;
        }
        else spawnIndex = 0;
        Vector3 moveDirection = myCamera.transform.forward;
        currentPick.GetComponent<Rigidbody>().AddForce( moveDirection * miningSpeed);
    }

    public void SetPickaxeCollected()
    {
        pickaxeCollected = true;
        pickaxeImage.enabled = true;
    }

    public void StartGatherTimer()
    {
        _canGather = false;
    }

    public bool GetGatherStatus()
    {
        return _canGather;
    }

    public void SetSwingState(bool state)
    {
        canSwing = state;
    }

    public bool GetSwingState()
    {
        return canSwing;
    }
}