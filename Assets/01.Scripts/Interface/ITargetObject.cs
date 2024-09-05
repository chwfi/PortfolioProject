using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetObject
{
    public GameObject Object { get; }
    public void DestroyObject();
}
