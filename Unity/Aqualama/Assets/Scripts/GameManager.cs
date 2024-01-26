using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Case startCase;
    public SpawnPosition startSpawnPosition;

    [Header("Debug")]
    public Case currentCase;
    public Collider2D currentMoveZone;

    private Zone[] zones;

    void Awake()
    {
        Instance = this;
        zones = GetComponentsInChildren<Zone>(true);
    }

    void Start()
    {
        Player.Instance.transform.position = startSpawnPosition.transform.position;
        CameraManager.Instance.MoveToCaseImmediate(currentCase);
        currentMoveZone = currentCase.moveZone;
        
    }

    public void ChangeActiveCase(Case newCase, string exitDirection){
        StartCoroutine(ChangeActiveCaseCoroutine(newCase, exitDirection));
    }

    private IEnumerator ChangeActiveCaseCoroutine(Case newCase, string exitDirection){
        currentCase = newCase;
        yield return StartCoroutine(CameraManager.Instance.MovetoCaseCoroutine(currentCase));

        SpawnPosition spawnPosition = currentCase.GetSpawnPosition(exitDirection);
        if(spawnPosition != null){
            Player.Instance.transform.position = spawnPosition.transform.position;
            SetActiveZone(currentCase);
        }
        else{
            Debug.LogError("No spawn position for direction " + exitDirection);
        }
        currentMoveZone = currentCase.moveZone;
        yield return null;
    }

    public void ChangeActiveCaseInfiniteStairs(Case newCase, string exitDirection, Transform target){
        StartCoroutine(ChangeActiveCaseInfiniteStairsCoroutine(newCase, exitDirection, target));
    }
    private IEnumerator ChangeActiveCaseInfiniteStairsCoroutine(Case newCase, string exitDirection, Transform target){
        Player.Instance.inputActive = false;
        float animDuration = 1f;
        float t = 0;
        Vector3 startPosition = Player.Instance.transform.position;
        Vector3 endPosition = target.position;
        Vector3 startScale = Player.Instance.transform.localScale;
        Vector3 endScale = target.localScale;

        while(t < animDuration){
            t += Time.deltaTime;
            float progress = t/animDuration;
            Player.Instance.transform.position = Vector3.LerpUnclamped(startPosition, endPosition, progress);
            Player.Instance.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, progress);
            yield return null;
        }
        yield return null;
        Player.Instance.inputActive = true;
    }

    private void SetActiveZone(Case newCase){
        foreach(Zone zone in zones){
            zone.isInZone = false;
        }

        foreach(Zone zone in newCase.zones){
            //Check if player is in zone collider bounds
            Vector3 playerPosition = Player.Instance.transform.position;
            Vector3 boundPos = new Vector3(playerPosition.x, playerPosition.y, 0);
            if(zone.collider2D.bounds.Contains(boundPos)){
                zone.isInZone = true;
            }
        }
        
    }


}
