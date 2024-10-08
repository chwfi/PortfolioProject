using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player/IsInputMove")]
public class IsInputMove : TransitionCondition
{
    public override bool IsConditionValid()
    {
        if (Owner.MoveCompo.InputReader.MoveInput.magnitude > 0) // 방향키 입력값이 감지되면 true 반환
            return true;
        else
            return false;
    }
}
