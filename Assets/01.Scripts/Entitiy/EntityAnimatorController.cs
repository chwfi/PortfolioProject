using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatorController : BaseComponent
{
    protected bool _animationEnd;
    public Animator Animator { get; private set; }

    protected virtual void Awake()
    {
        Animator = transform.GetComponent<Animator>();
    }

    public bool IsAnimationEnd()
    {
        bool value = _animationEnd;
        if(value)
            _animationEnd = false;
        return value;
    }

    public void AnimationAttackTrigger() // 정확한 공격 애니메이션 시점에 공격 기능을 수행하기 위해, 애니메이션 이벤트에서 실행
    {
        _owner.AttackCompo.OnAttack();
    }

    public void AnimationEndTrigger() // 애니메이션 이벤트에서 실행해주는 함수. 애니메이션 마지막 프레임에 실행된다
    {
        _animationEnd = true;
    }
}
