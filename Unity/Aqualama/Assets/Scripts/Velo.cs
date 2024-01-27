using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velo : MonoBehaviour
{
    public float forceAmount = 1000;
    public float torque= 1000;
    public float disableTriggerDuration=2f;
    private bool triggerDisabled = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start(){
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetVelo(){
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

                Vector2 force = forceAmount * new Vector2(1 * Random.Range(0.5f,1) * direction, 1 * Random.Range(0.5f,1));
                rb.AddForce(force);
                rb.angularVelocity = torque * Random.value;
                DisableTrigger();
            }
        }
    }

    private IEnumerator DisableTrigger(){
        triggerDisabled = true;
        yield return new WaitForSeconds(disableTriggerDuration);
        triggerDisabled = false;
    }

}
