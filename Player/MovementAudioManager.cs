using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAudioManager : MonoBehaviour
{
    [SerializeField]
    GameObject walkingAudioGameObject;
    AudioSource source;

    [SerializeField] float audioPlayDelay;

    [SerializeField]
    AudioClip[] footstepsAudioOnMetal;
    [SerializeField] float metalStepVolume;
    [SerializeField]
    AudioClip[] footstepsAudioOnDirt;
    [SerializeField] float dirtStepVolume;
    [SerializeField]
    AudioClip[] footstepsAudioOnStone;
    [SerializeField] float stoneStepVolume;
    [SerializeField]
    AudioClip[] footstepsAudioOnPuddle;
    [SerializeField] float puddleStepVolume;
    [SerializeField]
    AudioClip[] footstepsInDeepWater;
    [SerializeField] float deepStepVolume;

    void Start()
    {
        source = walkingAudioGameObject.GetComponent<AudioSource>();
    }

    public void PlayMetalFootsteps()
    {
        if (source.isPlaying) { return; }

        AudioClip clip = footstepsAudioOnMetal[Random.Range(0, footstepsAudioOnMetal.Length)];
        source.clip = clip;
        source.volume = metalStepVolume;
        source.PlayDelayed(audioPlayDelay);
    }

    public void PlayDirtFootsteps()
    {
        if (source.isPlaying) { return; }

        AudioClip clip = footstepsAudioOnDirt[Random.Range(0, footstepsAudioOnDirt.Length)];
        source.clip = clip;
        source.volume = dirtStepVolume;

        source.PlayDelayed(audioPlayDelay);
    }

    public void PlayStoneFootsteps()
    {
        if (source.isPlaying) { return; }

        AudioClip clip = footstepsAudioOnStone[Random.Range(0, footstepsAudioOnStone.Length)];
        source.clip = clip;
        source.volume = stoneStepVolume;

        source.PlayDelayed(audioPlayDelay);
    }

    public void PlayStoneFootsteps(float delayFactor)
    {
        if (source.isPlaying) { return; }

        AudioClip clip = footstepsAudioOnStone[Random.Range(0, footstepsAudioOnStone.Length)];
        source.clip = clip;
        source.volume = stoneStepVolume;

        source.PlayDelayed(audioPlayDelay * delayFactor);
    }

    public void PlayPuddleFootsteps()
    {
        if (source.isPlaying) { return; }

        AudioClip clip = footstepsAudioOnPuddle[Random.Range(0, footstepsAudioOnPuddle.Length)];
        source.clip = clip;
        source.volume = puddleStepVolume;

        source.PlayDelayed(audioPlayDelay);
    }

    public void PlayDeepWaterFootsteps()
    {
        if (source.isPlaying) { return; }

        AudioClip clip = footstepsInDeepWater[Random.Range(0, footstepsInDeepWater.Length)];
        source.clip = clip;
        source.volume = deepStepVolume;

        source.PlayDelayed(audioPlayDelay);
    }
}