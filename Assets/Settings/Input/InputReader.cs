using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input")]
public class InputReader : ScriptableObject, Controls.IPlayerMapActions
{
    public Vector2 MoveInput;
    public bool Attack;
    public bool Roll;

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack = true;
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Roll = true;
        }
    }
}