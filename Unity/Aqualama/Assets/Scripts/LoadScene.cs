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

    void Start()
    {
        faderSpriteRenderer.color = new Color(0,0,0,1);
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene(){
        yield return fader.FadeToWhiteCoroutine();
        yield return new WaitForSeconds(splashScreenDuration);
        yield return fader.FadeToBlackCoroutine();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        
    }
}
