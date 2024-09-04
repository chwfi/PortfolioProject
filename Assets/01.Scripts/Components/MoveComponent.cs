using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent : BaseComponent
{
    [Header("Speed")]
    [SerializeField] protected float _moveSpeed; // 이동속도

    [Header("Knockback")]
    [SerializeField] protected float _knockbackForce = 10f; // 넉백 크기
    [SerializeField] protected float _upwardForce = 5f; 

    public Rigidbody2D RigidbodyCompo { get; private set; }
    public Collider2D ColliderCompo { get; private set; }
    public InputReader InputReader { get; protected set; }
    
    private void Awake()
    {
        RigidbodyCompo = transform.GetComponent<Rigidbody2D>();
        ColliderCompo = transform.GetComponent<Collider2D>();
    }

    public void StopImmediately()
    {
        RigidbodyCompo.velocity = Vector2.zero;
    }

    public abstract void OnMove(); // 여기서 움직임 로직 실행
    public abstract void ApplyKnockback(Transform subject); // 넉백 로직 실행. subject는 때린 주체
    public abstract void Flip();
}
