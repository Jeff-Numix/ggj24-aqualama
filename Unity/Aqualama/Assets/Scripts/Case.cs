using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Case : MonoBehaviour
{
    public UnityEvent onEnterCase;
    public UnityEvent onExitCase;

    public CaseExit[] exits;
    public SpawnPosDirection[] spawnPositions;
    
    public Collider2D moveZone;

    [Header("Debug")]
    public Zone[] zones;

    void Start()
    {
        zones = GetComponentsInChildren<Zone>(true);
    }

    public void OnEnterCase(){
        if(onEnterCase != null){
            onEnterCase.Invoke();
        }
    }
    public void OnExitCase(){
        if(onExitCase != null){
            onExitCase.Invoke();
        }
    }
    public void ExitCase(string exitDirection){
        // Debug.Log("Exit case " + exitDirection);
        Case exitCase = GetExitCase(exitDirection);
        if(exitCase != null){
            GameManager.Instance.ChangeActiveCase(exitCase, exitDirection);
        }
    }

    public void ExitCaseInfiniteStairs(string exitDirection, Transform target, TargetStairs connectedStairs){
        // Debug.Log("Exit case " + exitDirection);
        Case exitCase = GetExitCase(exitDirection);
        if(exitCase != null){
            GameManager.Instance.ChangeActiveCaseInfiniteStairs(exitCase, exitDirection, target, connectedStairs);
        }
        else
        {
            Debug.LogError("No exit case for direction " + exitDirection);
        }
    }

    public Case GetExitCase(string exitDirection){
        foreach(CaseExit exit in exits){
            if(exit.direction == exitDirection){
                return exit.exitCase;
            }
        }
        return null;
    }

    public SpawnPosition GetSpawnPosition(string direction){
        foreach(SpawnPosDirection spawnPos in spawnPositions){
            if(spawnPos.direction == direction){
                return spawnPos.spawnPosition;
            }
        }
        return null;
    }

}

[System.Serializable]
public class CaseExit
{
    public string direction;
    public Case exitCase;
}
[System.Serializable]
public class SpawnPosDirection
{
    public string direction;
    public SpawnPosition spawnPosition;
}