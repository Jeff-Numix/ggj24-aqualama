using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case04_Parc : MonoBehaviour
{
    public Animator piafAnimator;
    public Zone piafZone;

    public void OnPlayerEnterPiafZone(){
        piafAnimator.SetTrigger("Fly");
        piafZone.gameObject.SetActive(false);
    }

    public void ResetCase(){
        piafAnimator.SetTrigger("Default");
        piafZone.gameObject.SetActive(true);
    }
}
