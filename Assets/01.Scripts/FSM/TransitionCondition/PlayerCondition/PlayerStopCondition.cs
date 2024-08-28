using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player_Stop")]
public class PlayerStopCondition : TransitionCondition
{
    private PlayerMove _playerMove => Owner.MoveCompo as PlayerMove;

    public override bool IsConditionValid()
    {
        if (_playerMove.Input.MoveInput.magnitude <= 0) // 방향키 입력값이 감지되지 않는다면 true 반환
            return true;
        else
            return false;
    }
}
