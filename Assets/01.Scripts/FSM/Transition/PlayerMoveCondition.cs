using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player_Move")]
public class PlayerMoveCondition : TransitionCondition
{
    private PlayerMove _playerMove => Owner.MoveCompo as PlayerMove;

    public override bool IsConditionValid()
    {
        if (_playerMove.Input.MoveInput.magnitude > 0)
            return true;
        else
            return false;
    }
}
