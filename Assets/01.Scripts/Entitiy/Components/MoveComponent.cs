using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] protected float _moveSpeed;

    public CharacterController CharacterControllerCompo { get; private set; }
    
    public virtual void Awake()
    {
        CharacterControllerCompo = transform.GetComponent<CharacterController>();
    }

    public abstract void OnUpdate();
}
