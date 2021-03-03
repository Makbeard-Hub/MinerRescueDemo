using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Identifier : MonoBehaviour
{
    [SerializeField] CurrentScene currentScene;

    public enum CurrentScene
    {
        D5, D4, D3, D2, D1
    }

    public CurrentScene GetSceneID()
    {
        return currentScene;
    }
}
