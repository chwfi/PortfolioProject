using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Test : TargetObject
{
    public override void DestroyObject()
    {
        QuestSystem.Instance.Report(this, 1);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DestroyObject();
        }
    }
}
