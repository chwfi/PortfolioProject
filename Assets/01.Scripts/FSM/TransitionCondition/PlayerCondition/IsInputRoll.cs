using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player/IsInputRoll")]
public class IsInputRoll : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.MoveCompo.InputReader.Roll;
    }
}
