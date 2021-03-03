using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted_Lift_Weight_Detection : MonoBehaviour
{
    [SerializeField]
    GameObject[] _weights;

    [SerializeField]
    int _objsToDestroy;
    int _objsDestroyed = 0;

    bool _liftCleared = false;

    // Update is called once per frame
    void Update()
    {
        _objsDestroyed = 0;
        foreach(GameObject obj in _weights)
        {
            if (obj == null)
                _objsDestroyed++;
        }

        if (_objsDestroyed == _objsToDestroy)
            _liftCleared = true;
    }

    public bool getClearedStatus()
    {
        return _liftCleared;
    }
}
