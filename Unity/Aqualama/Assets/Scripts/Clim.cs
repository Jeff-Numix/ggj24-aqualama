using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clim : MonoBehaviour
{
    public Rigidbody2D clim;
    private Vector3 climStartPosition;
    public AudioSource climFallAudioSource;
    public float fallYPosition = -1f;
    public Transform climParent;
    [Header("Blood")]
    public Transform bloodTransform;
    public float animDuration = 10;
    public float startScale = 1;
    public float endScale = 3f;
    public AnimationCurve bloodCurve;
    [Header("Debug")]
    public bool hasFallen=false;
    private bool hasCollided=false;

    void Start()
    {
        clim.transform.position = climParent.position;
        climStartPosition = clim.transform.position;
    }

    public void Fall(){
        clim.simulated = true;
        StopAllCoroutines();
        StartCoroutine(WaitUntilDisablePhysicsCoroutine());
    }
    void OnCollisionEnter2D(){
        if(!hasCollided){
            hasCollided=true;
            StartCoroutine(WaitUntilDisablePhysicsCoroutine());
            if(!climFallAudioSource.isPlaying){
                climFallAudioSource.Play();
            }
        }
    }
    public void Reset(){
        StopAllCoroutines();
        clim.simulated = false;
        clim.transform.position = climStartPosition;
        bloodTransform.transform.localScale = new Vector3(1, 1, 1);
        hasFallen=false;
        hasCollided=false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && !hasFallen){
            
            bloodTransform.transform.localScale = new Vector3(1, 1, 1);
            Player.Instance.PlayEcrabouilleAnim();
            GameManager.Instance.PlayerDie();
            if(!climFallAudioSource.isPlaying){
                climFallAudioSource.Play();
            }
            StartCoroutine(StartBloodAnimationCoroutine());
            
        }

    }

    private IEnumerator StartBloodAnimationCoroutine(){
        bloodTransform.gameObject.SetActive(true);
        float timer = 0;
        while(timer < animDuration){
            timer += Time.deltaTime;
            float ratio = timer / animDuration;
            float scale = Mathf.Lerp(startScale, endScale, bloodCurve.Evaluate(ratio));
            bloodTransform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
    }

    private IEnumerator WaitUntilDisablePhysicsCoroutine(){
        yield return new WaitForSeconds(1f);
        clim.simulated = false;
        hasFallen=true;
    }
}
