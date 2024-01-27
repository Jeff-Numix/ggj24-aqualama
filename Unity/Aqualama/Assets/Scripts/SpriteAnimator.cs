using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public bool loop=true;
    public float frameDuration=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnimation());
    }
    void OnEnable(){
        StartCoroutine(StartAnimation());
    }
    
    IEnumerator StartAnimation(){
        int i=0;
        while(true){
            spriteRenderer.sprite = sprites[i];
            i++;
            if(i>=sprites.Length){
                if(loop){
                    i=0;
                }else{
                    break;
                }
            }
            yield return new WaitForSeconds(frameDuration);
        }
    }
}
