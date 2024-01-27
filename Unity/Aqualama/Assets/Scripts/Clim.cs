using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clim : MonoBehaviour
{
    public Rigidbody2D clim;
    private Vector3 climStartPosition;

    void Start()
    {
         climStartPosition = clim.transform.position;
    }

    public void Fall(){
        clim.simulated = true;
    }

        public void Reset(){
        clim.simulated = false;
        clim.transform.position = climStartPosition;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Player.Instance.PlayEcrabouilleAnim();
            GameManager.Instance.PlayerDie();
        }
    }
}
