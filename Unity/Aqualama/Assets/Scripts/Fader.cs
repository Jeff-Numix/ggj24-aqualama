using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader Instance;

    public Color color;
    public float alphaFadeBlack = 1f;
    public float alphaFadeWhite = 0f;
    public float alphaFadeDead = 0.8f;
    public float fadeWhiteSpeed=1f;
    public float fadeBlackSpeed=0.5f;
    public float fadeDeadeSpeed=0.25f;
    SpriteRenderer spriteRenderer;

    void Awake(){
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color c = color;
        c.a=alphaFadeBlack;
        spriteRenderer.color = c;
    }

    public void FadeToBlack(){
        StartCoroutine(FadeToBlackCoroutine());
    }

    public IEnumerator FadeToBlackCoroutine(){
        float alpha = spriteRenderer.color.a;
        Color c = color;
        while(alpha < alphaFadeBlack){
            alpha += Time.deltaTime * fadeBlackSpeed;
            c.a=alpha;
            spriteRenderer.color = c;
            yield return null;
        }
        c.a=alphaFadeBlack;
        spriteRenderer.color = c;
    }

    public void FadeToWhite(){
        StartCoroutine(FadeToWhiteCoroutine());
    }

    public IEnumerator FadeToWhiteCoroutine(){
        float alpha = spriteRenderer.color.a;
        Color c = color;
        while(alpha > alphaFadeWhite){
            alpha -= Time.deltaTime * fadeWhiteSpeed;
            c.a=alpha;
            spriteRenderer.color = c;
            yield return null;
        }
        c.a=alphaFadeWhite;
        spriteRenderer.color = c;
    }
    public void FadeToDead(){
        StartCoroutine(FadeToDeadCoroutine());
    }
    public IEnumerator FadeToDeadCoroutine(){
        float alpha = spriteRenderer.color.a;
        Color c = color;
        while(alpha < alphaFadeDead){
            alpha += Time.deltaTime * fadeDeadeSpeed;
            c.a=alpha;
            spriteRenderer.color = c;
            yield return null;
        }    
        c.a=alphaFadeDead;
        spriteRenderer.color = c;
    }
}
