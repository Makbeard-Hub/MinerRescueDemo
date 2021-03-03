using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_Data : MonoBehaviour
{
    public int levelIndex;
    public int[] cardIndeces;
    public int[] codeIndeces;
    public bool pickAxeAcquired;

    public Player_Data()
    {
        levelIndex = 0;
        cardIndeces = new int[7];
        codeIndeces = new int[4];
        pickAxeAcquired = false;
    }
}
