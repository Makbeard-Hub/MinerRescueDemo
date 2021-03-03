using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    float health = 100;

    float maxHealth = 100;

    [SerializeField] [Range(0,100)]
    float healsPerSecond = 1f;

    [SerializeField]
    Image damageOverlayImage;

    float _damageCD = 1;
    float healTimer = 1;

    bool _recentlyDamaged = false;
    bool _dead = false;

    GameObject _player;
    GameObject _mainCam;
    Mouse_Look _playerMouseScript;
    Mouse_Look _cameraMouseScript;

    Color newColor;
    float alphaAmnt;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMouseScript = _player.GetComponent<Mouse_Look>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraMouseScript = _mainCam.GetComponent<Mouse_Look>();

        if(damageOverlayImage == null)
        {
            damageOverlayImage = GameObject.FindGameObjectWithTag("Health Image").GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the character was recently damaged, decrement the cooldown timer
        if (_recentlyDamaged)
            _damageCD -= Time.deltaTime;

        //If the cooldown hits 0, reset it and uncheck the recently damaged bool
        if (_damageCD <= 0)
        {
            _damageCD = 1;
            _recentlyDamaged = false;
        }

        healTimer -= Time.deltaTime;
        if (healTimer <= 0)
        {
            healTimer = 1;
            HealPlayer(healsPerSecond);
        }

        RegulateHealthLevels();

        AdjustDamageOverlay();

        if (health <= 0)
            KillCharacter();
    }

    private void RegulateHealthLevels()
    {
        health = Mathf.Min(health, maxHealth);
        health = Mathf.Max(health, 0);
    }

    private void HealPlayer(float healAmnt)
    {
        health += healAmnt;
    }

    private void AdjustDamageOverlay()
    {
        newColor = damageOverlayImage.color;

        alphaAmnt = (maxHealth - health) / maxHealth;
        newColor = new Color(newColor.r, newColor.g, newColor.b, alphaAmnt);
        //Debug.Log("Alpha amnt is: " + alphaAmnt);
        //Debug.Log("Health amnt is: " + health);

        damageOverlayImage.color = newColor;
    }

    public void ReduceHealth(float damageValue)
    {
        if(_recentlyDamaged == false)
        {
            health -= damageValue;
            _recentlyDamaged = true;
        }

    }

    void KillCharacter()
    {
        //For now, freezes time and indicates who died (effectively acting as a game over)
        Time.timeScale = 0;

        //Freeze the camera rotation so the player can't keep moving it
        _playerMouseScript.enabled = false;
        _cameraMouseScript.enabled = false;

        GameObject _messageTextObj = GameObject.FindGameObjectWithTag("Message Text");
        Text _messageText = _messageTextObj.GetComponent<Text>();
        _messageText.enabled = true;
        _messageText.text = gameObject.name + " was killed!";
    }

    public bool IsDead()
    {
        return _dead;
    }
}
