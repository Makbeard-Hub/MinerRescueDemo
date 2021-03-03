using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight_Boulder : MonoBehaviour
{
    [SerializeField]
    Renderer outlineEffect;
    [SerializeField] float mouseOverRange = 5f;

    bool useEffect = true;

    GameObject player;
    Mine_Object mine_Object;

    // Start is called before the first frame update
    void Start()
    {
        outlineEffect.enabled = false;

        if (gameObject.tag == "Mineable")
        {
            useEffect = true;
        }
        else
            useEffect = false;
    }

    private void OnMouseOver()
    {
        if (useEffect)
        {
            if(mine_Object != null)
            {
                if (WithinRange())
                {
                    mine_Object.SetSwingState(true);
                    outlineEffect.enabled = true;
                }
                else
                {
                    mine_Object.SetSwingState(false);
                    outlineEffect.enabled = false;
                }
            }
            else
            {
                mine_Object = FindObjectOfType<Mine_Object>();
            }

        }
    }

    private bool WithinRange()
    {
        if (Vector3.Distance(gameObject.transform.position, mine_Object.transform.position) <= mouseOverRange)
        {
            return true;
        }
        else return false;
    }

    private void OnMouseExit()
    {
        if (useEffect)
        {
            if(mine_Object != null)
            {
                mine_Object.SetSwingState(false);
            }
            else
            {
                Debug.Log("There is no Mine_Object found!!!");
            }
            outlineEffect.enabled = false;
        }
    }
}