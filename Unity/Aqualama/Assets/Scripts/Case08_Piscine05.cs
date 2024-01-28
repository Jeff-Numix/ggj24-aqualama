using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case08_Piscine05 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterJumpZone(){
        Debug.Log("EnterJumpZone");
        Player.Instance.PlayJumpPiscine();
    }
}
