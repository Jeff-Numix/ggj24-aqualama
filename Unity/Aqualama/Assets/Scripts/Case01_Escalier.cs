using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Case01_Escalier : MonoBehaviour
{

    public GameObject[] planches;
    public GameObject zonePorte;
    public Animator animatorEclairage;

    public int numStepsToUnlockDoor=3;
    [Header("Debug")]
    public string[] stepSequence;
    public int stepCount=0;

    void Start()
    {
        zonePorte.SetActive(false);
        stepCount=0;

        //Generate random sequence
        stepSequence = new string[numStepsToUnlockDoor];
        for (int i = 0; i < stepSequence.Length; i++)
        {
            stepSequence[i] = Random.Range(0,2)==0 ? "UP" : "DOWN";
        }
    }

    public void OnEnterCase(){
        if(GameManager.Instance.useDebugCase){
            zonePorte.SetActive(true);
            for (int i = 0; i < planches.Length; i++)
            {
                planches[i].gameObject.SetActive(false);
            }
            return;
        }
        if(stepCount>=numStepsToUnlockDoor){
            animatorEclairage.SetTrigger("Disco");
            GameManager.Instance.mainMusic.FadeOut();
            GameManager.Instance.discoMusic.FadeIn();
        }
    }

    public void OnFloorChanged(string direction){

        if(stepCount>=numStepsToUnlockDoor){
            return;
        }
        // Debug.Log("OnCaseSetup stairCount:"+stepCount);
        string currentStep = stepSequence[stepCount];
        if(currentStep==direction){
            stepCount++;
        }
        else {
            // stepCount=0;
        }


        for (int i = 0; i < planches.Length; i++)
        {
            if(i<stepCount){
                Rigidbody2D rb = planches[i].GetComponent<Rigidbody2D>();
                rb.simulated = true;
            }

        }
        
        if(stepCount>=numStepsToUnlockDoor){
            zonePorte.SetActive(true);
            animatorEclairage.SetTrigger("Disco");
            GameManager.Instance.mainMusic.FadeOut();
            GameManager.Instance.discoMusic.FadeIn();
        }
        else {
            zonePorte.SetActive(false);
            animatorEclairage.SetTrigger("Default");
        }

        

    }
}
