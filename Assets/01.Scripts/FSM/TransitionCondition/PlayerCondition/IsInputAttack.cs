using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player/IsInputAttack")]
public class IsInputAttack : TransitionCondition
{
    private PlayerMove _player => Owner.MoveCompo as PlayerMove;

    public override bool IsConditionValid()
    {
        bool b = _player.Input.Attack;
        if(b)
            _player.Input.Attack = false;
        return b;
    }
}
