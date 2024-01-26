using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    public float moveSpeed=10f;

    [Header("Debug")]
    public bool inputActive=true;

    void Awake()
    {
        Instance=this;
        input = new CustomInput();
        
    }

    void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed  += OnMovementPerformed;
        input.Player.Move.canceled   += OnMovementCanceled;
    }
    
    void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed  -= OnMovementPerformed;
        input.Player.Move.canceled   -= OnMovementCanceled;
    }

    void Update()
    {
        if(inputActive){
            // Manage horizontal move
            Vector3 newPosition = transform.position + new Vector3(moveVector.x, 0, 0) * Time.deltaTime * moveSpeed;
            Vector3 boundPos = new Vector3(newPosition.x, transform.position.y, 0);
            // Vérifiez si la nouvelle position est à l'intérieur du Collider2D de la zone de déplacement.
            Collider2D moveZone = GameManager.Instance.currentMoveZone;
            if(moveZone!=null){
                if (moveZone.bounds.Contains(boundPos))
                {
                    transform.position = newPosition;
                }
            }
            else {
                transform.position = newPosition;
                Debug.Log("No move zone");
            }
        }

        
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }
}
