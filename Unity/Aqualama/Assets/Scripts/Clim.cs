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

    void Start()
    {
        clim.transform.position = climParent.position;
        climStartPosition = clim.transform.position;
    }

    public void Fall(){
        clim.simulated = true;
        StartCoroutine(WaitUntilDisablePhysicsCoroutine());
    }
    void Update(){
        if(hasFallen=false && clim.transform.localPosition.y < fallYPosition){
            
            hasFallen=true;
            if(!climFallAudioSource.isPlaying){
                climFallAudioSource.Play();
            }
        }
    }
    public void Reset(){
        clim.simulated = false;
        clim.transform.position = climStartPosition;
        hasFallen=false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && !hasFallen){
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
