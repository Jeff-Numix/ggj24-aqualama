using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetStairs : MonoBehaviour
{
    public enum MoveDirection
    {
        Up,
        Down
    }
    public MoveDirection moveDirection = MoveDirection.Up;
    public TargetStairs connectedStairs;
    private Case parentCase;
    // Start is called before the first frame update
    void Start()
    {
        parentCase = GetComponentInParent<Case>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDirection == MoveDirection.Up)
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("Up");
                parentCase.ExitCaseInfiniteStairs("UP", transform, connectedStairs);
                gameObject.SetActive(false);
            }
        }
        else{
            if(Input.GetAxis("Vertical") < 0)
            {
                Debug.Log("Down");
                parentCase.ExitCaseInfiniteStairs("DOWN", transform, connectedStairs);
                gameObject.SetActive(false);
            }
        }

    }
}
