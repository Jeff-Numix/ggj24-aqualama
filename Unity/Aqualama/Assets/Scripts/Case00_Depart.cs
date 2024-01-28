using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case00_Depart : MonoBehaviour
{
    public Transform bananaTransform;

    void Start()
    {
        bananaTransform.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ResetCase(){
        bananaTransform.transform.localPosition = new Vector3(0, 0, 0);
    }

}
