using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncingObject : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    public float forceAmountX = 800;
    public float forceAmountY = 800;
    public float torque= 1000;
    public float disableTriggerDuration=2f;
    private bool triggerDisabled = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start(){
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Reset(){
        transform.position = startPosition;
        transform.rotation = startRotation;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        triggerDisabled=false;
        StopAllCoroutines();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(!triggerDisabled){
            if(other.gameObject.tag == "Player"){
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

                int direction = -(int)Mathf.Sign(other.transform.position.x - transform.position.x);

                Vector2 force =  new Vector2(forceAmountX * Random.Range(0.5f,1) * direction, forceAmountY * Random.Range(0.5f,1));
                rb.AddForce(force);
                rb.angularVelocity = torque * Random.value;
                StartCoroutine (DisableTrigger());

                if(onTriggerEnter!=null){
                    onTriggerEnter.Invoke();
                }
            }
        }
    }

    private IEnumerator DisableTrigger(){
        triggerDisabled = true;
        yield return new WaitForSeconds(disableTriggerDuration);
        triggerDisabled = false;
    }

}
