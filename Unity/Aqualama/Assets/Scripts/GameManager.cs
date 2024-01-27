using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Case startCase;
    public SpawnPosition startSpawnPosition;

    public bool useDebugCase;
    public Case debugStartCase;
    public SpawnPosition debugStartSpawnPosition;

    [Header("Debug")]
    public Case currentCase;
    public Collider2D currentMoveZone;

    private Zone[] zones;

    private bool playerIsDead=false;

    void Awake()
    {
        Instance = this;
        zones = GetComponentsInChildren<Zone>(true);
    }

    void Start()
    {
        Case myCase = useDebugCase ? debugStartCase : startCase;
        SpawnPosition mySpawnPosition = useDebugCase ? debugStartSpawnPosition : startSpawnPosition;

        Player.Instance.transform.position = mySpawnPosition.transform.position;
        Player.Instance.lamaLevelScale.localScale = new Vector3(myCase.playerScale, myCase.playerScale, 1);
        currentCase = myCase;

        CameraManager.Instance.MoveToCaseImmediate(currentCase);
        currentMoveZone = currentCase.moveZone;
        
    }

    public void PlayerDie(){
        playerIsDead=true;
    }

    void Update(){
        if(playerIsDead && Input.GetKeyDown(KeyCode.Space)){
            //Reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            playerIsDead=false;
        }
    }

    public void ChangeActiveCase(Case newCase, string exitDirection){
        StartCoroutine(ChangeActiveCaseCoroutine(newCase, exitDirection));
    }

    private IEnumerator ChangeActiveCaseCoroutine(Case newCase, string exitDirection){
        yield return StartCoroutine(CameraManager.Instance.MovetoCaseCoroutine(newCase));
        currentCase.OnExitCase();
        currentCase = newCase;

        newCase.OnEnterCase();
        SpawnPosition spawnPosition = currentCase.GetSpawnPosition(exitDirection);
        if(spawnPosition != null){
            Player.Instance.transform.position = spawnPosition.transform.position;
            Player.Instance.lamaLevelScale.localScale = new Vector3(newCase.playerScale, newCase.playerScale, 1);
            SetActiveZone(currentCase);
        }
        else{
            Debug.LogError("No spawn position for direction " + exitDirection);
        }
        currentMoveZone = currentCase.moveZone;
        yield return null;
    }

    public void ChangeActiveCaseInfiniteStairs(Case newCase, string exitDirection, Transform target, TargetStairs connectedStairs){
        StartCoroutine(ChangeActiveCaseInfiniteStairsCoroutine(newCase, exitDirection, target, connectedStairs));
    }
    private IEnumerator ChangeActiveCaseInfiniteStairsCoroutine(Case newCase, string exitDirection, Transform target, TargetStairs connectedStairs){
        
        Player.Instance.inputActive = false;
        float animDuration = 0.6f;
        float t = 0;
        Vector3 startPosition = Player.Instance.transform.position;
        Vector3 endPosition = new Vector3(target.position.x, target.position.y, Player.Instance.transform.position.z);
        Vector3 startScale = Player.Instance.transform.localScale;
        Vector3 endScale = target.localScale;
        Player.Instance.PlayStairsAnim();
        while(t < animDuration){
            t += Time.deltaTime;
            float progress = t/animDuration;
            Player.Instance.transform.position = Vector3.LerpUnclamped(startPosition, endPosition, progress);
            Player.Instance.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, progress);
            yield return null;
        }
        Player.Instance.gameObject.SetActive(false);
        

        yield return StartCoroutine(CameraManager.Instance.MovetoCaseCoroutine(newCase));
        currentCase.GetComponent<Case01_Escalier>().OnFloorChanged(exitDirection);
        currentCase.OnExitCase();
        Player.Instance.gameObject.SetActive(true);
        //Move back to previous Case immediately
        CameraManager.Instance.MoveToCaseImmediate(currentCase);
        // Player.Instance.transform.position = startSpawnPosition.transform.position;
        SpawnPosition spawnPosition = currentCase.GetSpawnPosition(exitDirection);
        // Debug.Log("Spawn position " + spawnPosition.name);
        if(spawnPosition != null){

            startPosition = connectedStairs.transform.position;
            startScale = connectedStairs.transform.localScale;
            endPosition = spawnPosition.transform.position;
            endScale = spawnPosition.transform.localScale;
            t = 0;
            Player.Instance.PlayStairsAnim();
            while(t < animDuration){
                t += Time.deltaTime;
                float progress = t/animDuration;
                Player.Instance.transform.position = Vector3.LerpUnclamped(startPosition, endPosition, progress);
                Player.Instance.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, progress);
                yield return null;
            }
            
            SetActiveZone(currentCase);
        }
        else{
            Debug.LogError("No spawn position for direction " + exitDirection);
        }
        Player.Instance.GoIdle();

        yield return null;
        Player.Instance.inputActive = true;
    }

    private void SetActiveZone(Case newCase){
        foreach(Zone zone in zones){
            zone.isInZone = false;
            zone.ForceExitZone();
        }

        foreach(Zone zone in newCase.zones){
            //Check if player is in zone collider bounds
            Vector3 playerPosition = Player.Instance.transform.position;
            Vector3 boundPos = new Vector3(playerPosition.x, playerPosition.y, 0);
            if(zone.collider2D.bounds.Contains(boundPos)){
                zone.ForceEnterZone();
            }
        }
        
    }


}
