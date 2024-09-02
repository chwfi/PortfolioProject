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

        Transform targetTrm = _owner.TargetCompo.GetTarget().transform;
        Vector2 knockbackDirection = (_owner.transform.position - targetTrm.position).normalized;
        _owner.MoveCompo.ApplyKnockback(knockbackDirection);
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
