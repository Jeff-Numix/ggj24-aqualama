using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clim : MonoBehaviour
{
    public Rigidbody2D clim;
    private Vector3 climStartPosition;

    [Header("Blood")]
    public Transform bloodTransform;
    public float animDuration = 10;
    public float startScale = 1;
    public float endScale = 3f;
    public AnimationCurve bloodCurve;
    private bool hasFallen=false;

    void Start()
    {
         climStartPosition = clim.transform.position;
    }

    public void Fall(){
        clim.simulated = true;
        StartCoroutine(WaitUntilDisablePhysicsCoroutine());
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
