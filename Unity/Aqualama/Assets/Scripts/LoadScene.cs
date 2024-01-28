using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int sceneIndexToLoad=1;


    public float splashScreenDuration=2;
    public float splashScreenFadeDuration=2;
    public Fader fader;
    public SpriteRenderer faderSpriteRenderer;
    public AudioSource startGameAudio;

    void Start()
    {
        fader.gameObject.SetActive(true);
        faderSpriteRenderer.color = new Color(0,0,0,1);
        StartCoroutine(LoadIntro());

    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(LoadTheScene());
        }
    }

    IEnumerator LoadIntro(){
        yield return fader.FadeToWhiteCoroutine();

    }

    IEnumerator LoadTheScene(){
        startGameAudio.Play();
        yield return fader.FadeToBlackCoroutine();
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
