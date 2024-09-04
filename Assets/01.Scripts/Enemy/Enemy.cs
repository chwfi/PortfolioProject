using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITargetObject
{
    public PlayerController Target { get; protected set; }

    public GameObject Object => this.gameObject;

    protected override void Awake()
    {
        base.Awake();

        TargetCompo = transform.GetComponent<TargetComponent>();
    }

    public override void EntityDead()
    {
        base.EntityDead();
        ReportObject();
    }

    public void ReportObject()
    {
        QuestSystem.Instance.Report(this, 1);
    }
}
