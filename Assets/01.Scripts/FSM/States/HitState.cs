using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    public HitState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _owner.MoveCompo.ApplyKnockback(_owner.HealthCompo.HitSubject);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
