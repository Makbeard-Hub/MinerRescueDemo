using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This script is attached to the pickaxe and requires it to have a trigger box collider
 * this detects if the collided object is mineable, if the animation is currently mining, and if the player is allowed to gather
 * It communicates with Mine_Object to get a gather bool status, as well as beginning timer.
 * Script created by M. Justice 3/6/2020
 * Changes made to use a minipickaxe and spawn audio and particle sounds at point of collision. Derenders pickaxe then deletes after delay.
 * Edited by M. Justice on 9/16/2020
 */
public class PickAxe : MonoBehaviour
{
    Mine_Object miner;
    GameObject player;
    Animator anim;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField] AudioClip[] miningSFX;
    [SerializeField] float lifetime = 2f;

    [SerializeField] GameObject particles;

    float lifeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        miner = FindObjectOfType<Mine_Object>();
        player = FindObjectOfType<Player_Controller_v1>().gameObject;
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    //When pickaxe hits a mineable object, 
    //Then reduce object health/durability
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        Vector3 contactPoint = collision.GetContact(0).point;

        if (other.tag == "Mineable")
        {
            other.GetComponent<Mineable_Object>().LoseHP();

            PlayMineAudio(contactPoint);
            DoParticleEffects(contactPoint);
            RemovePickAxe();
        }
        else
        {
            PlayMineAudio(contactPoint);
            RemovePickAxe();
        }
    }

    private void DoParticleEffects(Vector3 playPoint)
    {
        Instantiate(particles, playPoint, transform.rotation);
    }

    private void RemovePickAxe()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var rend in renderers)
        {
            rend.enabled = false;
        }
        Destroy(this.gameObject, 2f);
    }

    private void PlayMineAudio(Vector3 point)
    {
        AudioClip clip = miningSFX[UnityEngine.Random.Range(0, miningSFX.Length)];

        audioSource.clip = clip;
        audioSource.transform.position = point;
        audioSource.Play();
    }
}