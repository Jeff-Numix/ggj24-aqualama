using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float minRot=-20;
    public float maxRot=20;
    float t=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime*Input.GetAxis("Horizontal")*rotationSpeed;
        float angle = Mathf.PingPong(t, maxRot-minRot)+minRot;
        transform.localRotation = Quaternion.Euler(0,0,angle);

    }
}
