using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveComponent : BaseComponent
{
    [Header("Speed")]
    [SerializeField] protected float _moveSpeed; // 이동속도

    [Header("Knockback")]
    [SerializeField] float _knockbackForce = 10f; // 넉백 크기
    [SerializeField]float _upwardForce = 5f; 

    public Rigidbody2D RigidbodyCompo { get; private set; }
    public InputReader InputReader { get; protected set; }
    
    private void Awake()
    {
        RigidbodyCompo = transform.GetComponent<Rigidbody2D>();
    }

    public void StopImmediately()
    {
        RigidbodyCompo.velocity = Vector2.zero;
    }

    public void ApplyKnockback(Vector2 direction)
    {
        direction.Normalize();
        Vector2 knockback = direction * _knockbackForce;
        knockback.y += _upwardForce;
        _owner.MoveCompo.RigidbodyCompo.AddForce(knockback, ForceMode2D.Impulse);
    }

    public abstract void OnMove(); // 여기서 움직임 로직 실행
    public abstract void Flip();
}
