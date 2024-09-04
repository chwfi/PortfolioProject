using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatorController : BaseComponent
{
    public Animator Animator { get; private set; }

    protected virtual void Awake()
    {
        Animator = transform.GetComponent<Animator>();
    }

    public void AnimationAttackTrigger() // 정확한 공격 애니메이션 시점에 공격 기능을 수행하기 위해, 애니메이션 이벤트에서 실행
    {
        var skill = _owner.SkillManagerCompo.GetSkill(SkillTypeEnum.Attack);
        if (skill != null)
            skill.PlaySkill();
    }

    public void AnimationEndTrigger() // 애니메이션 이벤트에서 달아주는 함수. 애니메이션 마지막 프레임에 실행된다
    {
        _owner.StateMachineCompo.ChangeState(StateTypeEnum.Idle);
    }
}
