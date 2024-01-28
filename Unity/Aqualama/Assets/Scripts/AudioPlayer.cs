using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    public bool IsPlaying{ get { return audioSource.isPlaying; }}

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(){
        if(audioClips.Length > 0){
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }
    }
}
