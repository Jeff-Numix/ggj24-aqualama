using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMamie : MonoBehaviour
{
    public ParticleSystem theParticleSystem;
    public SpriteRenderer checkSpriteRenderer;
    public float delayBad=4;
    public float delayOK=1;
    public AudioPlayer audioPlayer;
    [Header("Debug")]
    public bool isPetActive=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnterCase(){
        GameManager.Instance.mainMusic.FadeIn();
        GameManager.Instance.discoMusic.FadeOut();
    }

    IEnumerator PetActiveCoroutine(){
        while(true){
            isPetActive=true;
            checkSpriteRenderer.color = Color.red;
            audioPlayer.Play();
            yield return new WaitForSeconds(delayBad);
            isPetActive=false;
            checkSpriteRenderer.color = Color.green;
            yield return new WaitForSeconds(delayOK);
        }
    }
    public void StartProutSequence(){
        theParticleSystem.Play();
        StartCoroutine(PetActiveCoroutine());
    }
    public void StopProutSequence(){
        theParticleSystem.Stop();
        StopAllCoroutines();
    }

    public void CheckPet(){
        if(isPetActive){
            Player.Instance.PlayDieProut();
            GameManager.Instance.PlayerDie();
        }
    }

}
