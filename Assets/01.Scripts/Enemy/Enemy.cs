using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITargetObject
{
    public PlayerController Target { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
   
        TargetCompo = transform.GetComponent<TargetComponent>();
    }

    public override void EntityDead()
    {
        base.EntityDead();
        DestroyObject();
    }

    public void DestroyObject()
    {
        QuestSystem.Instance.Report(this, 1);
    }
}
