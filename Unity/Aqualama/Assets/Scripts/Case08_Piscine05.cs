using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case08_Piscine05 : MonoBehaviour
{
    public float endGameDelay=4f;
    public float soundHappyDelay=2f;
    public float soundSplashDelay=4f;
    public AudioSource audioSourceJumpSplash;
    public AudioSource audioSourceHappy;
    public ParticleSystem particleSystemSplash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterJumpZone(){
        Debug.Log("EnterJumpZone");
        Player.Instance.PlayJumpPiscine();
        StartCoroutine(EndGameCoroutine());
    }

    public IEnumerator EndGameCoroutine(){
        GameManager.Instance.gameIsEnded=true;
        yield return new WaitForSeconds(soundHappyDelay);
        audioSourceHappy.Play();
        yield return new WaitForSeconds(soundSplashDelay);
        particleSystemSplash.Play();
        audioSourceJumpSplash.Play();
        yield return new WaitForSeconds(endGameDelay);
        GameManager.Instance.ShowEndGameUI();
    }
}
