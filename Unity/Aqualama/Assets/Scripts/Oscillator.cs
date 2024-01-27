using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float oscillationSpeed = 1f;
    public Vector2 oscillationOffset = new Vector2(0f, 1f);
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // OSCILLATE the Y position of the indicator over time
        float x = Mathf.Sin(Time.time * oscillationSpeed) * oscillationOffset.x;
        float y = Mathf.Sin(Time.time * oscillationSpeed) * oscillationOffset.y;
        
        transform.position = startPosition + new Vector3(x,y, startPosition.z);
    }
}
