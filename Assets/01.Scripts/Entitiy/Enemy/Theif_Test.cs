using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theif_Test : MonoBehaviour, ITargetObject
{
    public GameObject Owner => this.gameObject;

    public void DestroyObject()
    {
        QuestSystem.Instance.Report(this, 1);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DestroyObject();
        }
    }
}
