using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Case01_Escalier : MonoBehaviour
{

    public GameObject[] planches;
    public GameObject zonePorte;
    

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
        }
        else {
            zonePorte.SetActive(false);
        }

        

    }
}
