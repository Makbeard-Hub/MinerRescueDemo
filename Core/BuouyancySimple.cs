using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuouyancySimple : MonoBehaviour
{
   
    const float WATER_DENSITY = 9.8f;
    private float volumeWaterDisplacedByObject = 5;
    public float magnitude;
    public float minDistance = 1;

    float objectMass;

    Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        objectMass = rbody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FloatObject(float waterHeight)
    {
        float objectsHeight = transform.position.y;

        if(objectsHeight - waterHeight < minDistance)
        {
            float ForceDown = Physics.gravity.magnitude * objectMass;
            float ForceUp = (waterHeight - objectsHeight) * volumeWaterDisplacedByObject * magnitude * Physics.gravity.magnitude;

            rbody.AddForce(Vector3.up * (ForceUp - ForceDown));
        }
    }
}
