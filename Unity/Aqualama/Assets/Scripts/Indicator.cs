using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public bool hideOnStart = true;

    void Start()
    {
        gameObject.SetActive(!hideOnStart);
    }


}
