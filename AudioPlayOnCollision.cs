using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class was created for general use in the situation where you would like to play
 * a sound effect generated at point of contact with another object. Needs a separate Audio Source gameObject to work properly.
 * Created by M. Justice 9/28/2020
 */

public class AudioPlayOnCollision : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip[] audioClipSet;

    [SerializeField] string colldiersTag;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        Vector3 contactPoint = collision.GetContact(0).point;

        if (colldiersTag != null && other.tag == colldiersTag)
        {
            PlayAudioAtPoint(audioClipSet, contactPoint);
        }
    }

    private void PlayAudioAtPoint(AudioClip[] audioClips, Vector3 point)
    {
        AudioClip clip = audioClipSet[UnityEngine.Random.Range(0, audioClipSet.Length)];

        audioSource.clip = clip;
        audioSource.transform.position = point;
        audioSource.Play();
    }

}
