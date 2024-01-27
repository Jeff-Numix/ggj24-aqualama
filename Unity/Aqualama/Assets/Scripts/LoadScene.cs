using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int sceneIndexToLoad=1;
    public float delayLoad=0.5f;

    void Start()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene(){

        yield return new WaitForSeconds(delayLoad);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        
    }
}
