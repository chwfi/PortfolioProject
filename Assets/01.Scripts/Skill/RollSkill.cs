using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

[CreateAssetMenu(menuName = "SO/Skill/RollSkill", fileName = "Skill_")]
public class RollSkill : Skill
{
    [Header("Stats")]
    [SerializeField] private float _rollSpeed;
    [SerializeField] private float _rollTime;

    public override void PlaySkill()
    {
        if (!Available) return;
        _owner.HealthCompo.Undamagable = true;
        Vector2 rollDirection = _owner.MoveCompo.InputReader.MoveInput.normalized;

        // MoveInput이 Vector2.zero일 때, 현재 바라보는 방향으로 설정
        if (rollDirection == Vector2.zero)
        {
            if (_owner.transform.localScale.x == 1)
                rollDirection = _owner.transform.right;
            else
                rollDirection = -_owner.transform.right;
        }

        _owner.MoveCompo.RigidbodyCompo.velocity = rollDirection * _rollSpeed;

        Available = false;
        CoroutineUtil.CallWaitForSeconds(_coolDown, () => Available = true);
        CoroutineUtil.CallWaitForSeconds(_rollTime, () => _owner.HealthCompo.Undamagable = false);
    }
}
