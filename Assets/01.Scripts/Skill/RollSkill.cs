using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/RollSkill", fileName = "Skill_")]
public class RollSkill : Skill
{
    [Header("Stats")]
    [SerializeField] private float _rollSpeed;

    public override void PlaySkill()
    {
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
    }
}
