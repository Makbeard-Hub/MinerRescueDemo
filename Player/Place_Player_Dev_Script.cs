using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Player_Dev_Script : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject[] _playerLocations;

    [SerializeField]
    int _currentIndex = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.F9))
        {
            MovePlayerToNextLocation();
        }
    }

    public void MovePlayerToNextLocation()
    {
        _currentIndex++;

        if (_currentIndex >= _playerLocations.Length)
            _currentIndex = 0;

        _player.transform.position = _playerLocations[_currentIndex].transform.position;
    }
    
}
