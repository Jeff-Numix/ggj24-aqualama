using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case01_Escalier : MonoBehaviour
{
    public GameObject[] planches;
    public GameObject zonePorte;

    public int numStairsToUnlockDoor=3;
    public int stairCount=0;

    void Start()
    {
        stairCount=0;
    }

    public void OnCaseSetup(){
        
        Debug.Log("OnCaseSetup stairCount:"+stairCount);
        for (int i = 0; i < planches.Length; i++)
        {
            planches[i].SetActive(i>=stairCount);
        }
        
        if(stairCount>=numStairsToUnlockDoor){
            zonePorte.SetActive(true);
        }
        else {
            zonePorte.SetActive(false);
        }

        stairCount++;

    }
}
