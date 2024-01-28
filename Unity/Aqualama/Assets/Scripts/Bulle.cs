using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour
{
    public AnimationCurve scaleAnimationCurve;
    public float scaleDuration;

    public void Show(){
        gameObject.SetActive(true);
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine(){
        float t=0;
        while(t<scaleDuration){
            t+=Time.deltaTime;
            float scale = scaleAnimationCurve.Evaluate(t/scaleDuration);
            transform.localScale = new Vector3(scale,scale,scale);
            yield return null;
        }
    }

    public void Hide(){
        if(gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            StartCoroutine(HideCoroutine());
        }
    }

    IEnumerator HideCoroutine(){
        float t=0;
        while(t<scaleDuration){
            t+=Time.deltaTime;
            float scale = scaleAnimationCurve.Evaluate(1-t/scaleDuration);
            transform.localScale = new Vector3(scale,scale,scale);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
