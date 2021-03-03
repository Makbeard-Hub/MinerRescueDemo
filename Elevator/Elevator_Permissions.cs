using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;


public class Elevator_Permissions : MonoBehaviour
{
    [SerializeField] bool floor5Powered = false;
    [SerializeField] bool floor4Powered = false;
    [SerializeField] bool floor3Powered = false;
    [SerializeField] bool floor2Powered = false;
    [SerializeField] bool floor1Powered = false;

    private GameObject powerPylon;

    public bool GetPermissionToMove()
    {
        // Check Floor Number
        // Check if poweredTrue, else run PowerSearch
        // Assign floor powered true if true

        bool floorPowered = false;

        Scene_Identifier sceneId = FindObjectOfType<Scene_Identifier>();

        switch (sceneId.GetSceneID())
        {
            case Scene_Identifier.CurrentScene.D5:
                if (!floor5Powered)
                {
                    floor5Powered = SearchForPowerPylon();
                }
                floorPowered = floor5Powered;
                break;
            case Scene_Identifier.CurrentScene.D4:
                if (!floor4Powered)
                {
                    floor4Powered = SearchForPowerPylon();
                }
                floorPowered = floor4Powered;
                break;
            case Scene_Identifier.CurrentScene.D3:
                if (!floor3Powered)
                {
                    floor3Powered = SearchForPowerPylon();
                }
                floorPowered = floor3Powered;
                break;
            case Scene_Identifier.CurrentScene.D2:
                if (!floor2Powered)
                {
                    floor2Powered = SearchForPowerPylon();
                }
                floorPowered = floor2Powered;
                break;
            case Scene_Identifier.CurrentScene.D1:
                if (!floor1Powered)
                {
                    floor1Powered = SearchForPowerPylon();
                }
                floorPowered = floor1Powered;
                break;
            default:
                Debug.LogError("There are no assigned Scenes for the current Scene_Identifier in scene!");
                break;
        }

        return floorPowered;
    }

    private bool SearchForPowerPylon()
    {
        powerPylon = GameObject.FindObjectOfType<Power_For_Elevator>().gameObject;
        return powerPylon.GetComponent<Powered_Component>().GetPowerStatus();
    }
}