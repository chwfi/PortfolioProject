using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest/TaskTarget/Object", fileName = "TaskTarget_")]
public class ObjectTarget : TaskTarget
{
    [SerializeField] private GameObject _target;

    public override object Target => _target;

    public override bool IsTargetEqual(object target)
    {
        var targetObject = target as ITargetObject; // ITargetObject로 변환
        if (targetObject == null)
            return false;
        return targetObject.Owner.name.Contains(_target.name); // 게임오브젝트 타겟은 판별을 이름이 포함되어있는가로 함
        // 프리팹 생성 시 (Clone)으로 복제되어 정확한 이름으로 구별하기 힘들기 때문. name이 포함되어만 있다면 true를 반환
    }
}
