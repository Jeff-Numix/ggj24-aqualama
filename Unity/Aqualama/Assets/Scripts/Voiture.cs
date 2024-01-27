using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiture : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSourceCrash;

    public void GoCrash(){
        animator.SetTrigger("Crash");
        audioSourceCrash.Play();
    }
}
