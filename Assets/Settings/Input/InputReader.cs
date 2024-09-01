using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

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
            AttackCallback();
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Roll = true;
            RollCallback();
        }
    }

    private void AttackCallback() // 선입력, 과다입력 방지를 위한 콜백 함수들. 한 프레임 바로 뒤에 false시켜준다.
    {
        CoroutineUtil.CallWaitForOneFrame(() => Attack = false);
    }

    private void RollCallback()
    {
        CoroutineUtil.CallWaitForOneFrame(() => Roll = false);
    }
}