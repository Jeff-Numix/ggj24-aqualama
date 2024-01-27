using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float transitionDuration=1;
    public float zoomDuration=1;
    public AnimationCurve transitionCurve;
    public AnimationCurve zoomCurve;

    public static CameraManager Instance;
    private Camera _camera;

    void Awake() {
        Instance = this;
        _camera=GetComponent<Camera>();
    }

    public void MoveToCaseImmediate(Case exitCase){
        // Debug.Log("Move to case " + exitCase.name);
        Vector3 targetPos = new Vector3(exitCase.transform.position.x, exitCase.transform.position.y, transform.position.z);
        transform.position = targetPos;
    }

    public IEnumerator MovetoCaseCoroutine(Case exitCase){
        // Debug.Log("Move to case " + exitCase.name);
        float t = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(exitCase.transform.position.x, exitCase.transform.position.y, transform.position.z);
        while(t < transitionDuration){
            t += Time.deltaTime;
            float p = t/transitionDuration;
            float progress = transitionCurve.Evaluate(p);
            transform.position = Vector3.LerpUnclamped(startPosition, endPosition, progress);
            _camera.orthographicSize = zoomCurve.Evaluate(p);
            yield return null;
        }
        transform.position = endPosition;
        
    }

}
