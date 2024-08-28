using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/PlayerIdle")]
public class PlayerIdleCondition : TransitionCondition
{
    private PlayerMove _playerMove => Owner.MoveCompo as PlayerMove;

    public override bool IsConditionValid()
    {
        if (_playerMove == null)
            Debug.Log("엥");

        if (_playerMove.Input.MoveInput.magnitude > 0)
            return true;
        else
            return false;
    }
}
