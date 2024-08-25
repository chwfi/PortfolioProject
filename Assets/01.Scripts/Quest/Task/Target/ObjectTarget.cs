using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest/TaskTarget/Object", fileName = "TaskTarget_")]
public class ObjectTarget : TaskTarget
{
    [SerializeField] private TargetObject _target;

    public override object Target => _target;

    public override bool IsTargetEqual(object target)
    {
        var targetObject = target as TargetObject;
        if (targetObject == null)
            return false;
        return targetObject.name.Contains(_target.name);
    }
}
