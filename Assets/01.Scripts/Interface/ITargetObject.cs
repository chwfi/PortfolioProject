using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetObject
{
    public GameObject Owner { get; }
    public void DestroyObject();
}
