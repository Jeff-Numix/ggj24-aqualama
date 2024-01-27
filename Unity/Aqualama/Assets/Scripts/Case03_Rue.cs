using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case03_Rue : MonoBehaviour
{
    public Sprite spriteFeuVert;
    public Sprite spriteFeuRouge;
    public SpriteRenderer feuSpriteRenderer;
    public Clim clim;

    public Voiture voiture;
    public float voitreCrashDelay=0.25f;
   



    public float feuDurationMin=2f;
    public float feuDurationMax=3f;

    [Header("Debug")]
    public bool isFeuVert = true;

    private float feuDuration=0;

    void Start()
    {
        StartCoroutine(feuCoroutine());
       
    }

    public void OnEnterCase(){
        clim.Reset();
    }

    private IEnumerator feuCoroutine(){
        while(true){
            feuDuration = Random.Range(feuDurationMin,feuDurationMax);
            isFeuVert = !isFeuVert;
            feuSpriteRenderer.sprite = isFeuVert ? spriteFeuVert : spriteFeuRouge;
            yield return new WaitForSeconds(feuDuration);
        }
    }
    public void EnterWalkpad(){
        if(isFeuVert){
            clim.Fall();
        }
        else {
            voiture.GoCrash();
            StartCoroutine(PlayDeadVoitureCoroutine());
        }
    }

    IEnumerator PlayDeadVoitureCoroutine(){
        yield return new WaitForSeconds(voitreCrashDelay);
        Player.Instance.PlayDeadVoiture();
        GameManager.Instance.PlayerDie();
    }

    public void ResetCase(){
        clim.Reset();
        voiture.Reset();
    }

}
