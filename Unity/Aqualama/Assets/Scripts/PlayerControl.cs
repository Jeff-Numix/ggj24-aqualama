using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    public float moveSpeed=10f;
    private Collider2D moveZone;

    void Awake()
    {
        input = new CustomInput();
        moveZone = GameObject.FindWithTag("MoveZone").GetComponent<Collider2D>();
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
        // Manage horizontal move
        Vector3 newPosition = transform.position + new Vector3(moveVector.x, 0, 0) * Time.deltaTime * moveSpeed;
        Vector3 boundPos = new Vector3(newPosition.x, transform.position.y, 0);
        // Vérifiez si la nouvelle position est à l'intérieur du Collider2D de la zone de déplacement.
        if (moveZone.bounds.Contains(boundPos))
        {
            Debug.Log("New position is inside the move zone");
            transform.position = newPosition;
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
