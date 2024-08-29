using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player/IsInputAttack")]
public class IsInputAttack : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.MoveCompo.InputReader.Attack;
    }
}
