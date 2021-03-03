using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancySimpleWater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<BuouyancySimple>() != null)
        {
            float waterHeight = transform.position.y;
            other.GetComponent<BuouyancySimple>().FloatObject(waterHeight);
        }
    }
}
