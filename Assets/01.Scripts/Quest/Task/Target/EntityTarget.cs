using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest/TaskTarget/Object", fileName = "TaskTarget_")]
public class EntityTarget : TaskTarget
{
    [SerializeField] private string _target;

    public override object Target => _target;

    public override bool IsTargetEqual(object target)
    {
        var targetObject = target as Entity; // ITargetObject로 변환
        if (targetObject.CodeName == _target)
            return true;
        else
            return false;
    }
}
