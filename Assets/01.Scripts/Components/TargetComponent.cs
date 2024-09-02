using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetComponent : BaseComponent
{
    [SerializeField] protected float _detectRange;
    [SerializeField] protected float _stopRange;
    [SerializeField] protected LayerMask _targetLayer;

    public abstract Entity GetTarget();
    public abstract bool Targeting();
    public abstract bool IsTargetEnter();
}
