using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent : BaseComponent
{
    [Header("Value")]
    [SerializeField] protected float _moveSpeed; // 이동속도

    public Rigidbody2D RigidbodyCompo { get; private set; }
    
    private void Awake()
    {
        RigidbodyCompo = transform.GetComponent<Rigidbody2D>();
    }

    public void StopImmediately()
    {
        RigidbodyCompo.velocity = Vector2.zero;
    }

    public abstract void OnMove(); // 여기서 움직임 로직 실행
    public abstract void Flip();
}
