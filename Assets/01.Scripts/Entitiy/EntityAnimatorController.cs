using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimatorController : MonoBehaviour
{
    public Animator Animator { get; private set; }

    protected virtual void Awake()
    {
        Animator = transform.GetComponent<Animator>();
    }
}
