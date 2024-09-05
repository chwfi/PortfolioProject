using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log(this.gameObject.name);
    }
}
