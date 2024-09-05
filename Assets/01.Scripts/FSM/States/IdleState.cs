using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class IdleState : State
{
    public IdleState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _owner.MoveCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _owner.IsConditionsValid(StateTypeEnum.Move);
        _owner.IsConditionsValid(StateTypeEnum.Attack);
        _owner.IsConditionsValid(StateTypeEnum.Roll);
        _owner.IsConditionsValid(StateTypeEnum.Hit);
        _owner.IsConditionsValid(StateTypeEnum.Dead);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
