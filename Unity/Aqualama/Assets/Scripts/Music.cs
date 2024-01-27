using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource musicAudioSource;

    public void FadeOut(){
        StartCoroutine(FadeOutCoroutine());
    }
    
    public IEnumerator FadeOutCoroutine(){
        float t = 0;
        float duration = 1f;
        while(t < duration){
            t += Time.deltaTime;
            float progress = t/duration;
            musicAudioSource.volume = Mathf.Lerp(1, 0, progress);
            yield return null;
        }
        musicAudioSource.Stop();
    }
    public void FadeIn(){
        StartCoroutine(FadeInCoroutine());
    }
    public IEnumerator FadeInCoroutine(){
        float t = 0;
        float duration = 1f;
        if(!musicAudioSource.isPlaying){
            musicAudioSource.Play();
        }
        while(t < duration){
            t += Time.deltaTime;
            float progress = t/duration;
            musicAudioSource.volume = Mathf.Lerp(0, 1, progress);
            yield return null;
        }
    }   
}
