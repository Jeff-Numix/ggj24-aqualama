using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    public bool hideOnStart = true;

    void Start()
    {
        gameObject.SetActive(!hideOnStart);
    }


}
