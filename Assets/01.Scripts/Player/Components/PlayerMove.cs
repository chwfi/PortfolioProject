using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MoveComponent
{
    [Header("InputReader")]
    [SerializeField] private InputReader _input; // 인풋

    private int _lastDirection = 1;

    private void OnEnable() // InputReader 활성화 해주는 작업
    {
        InputReader = _input;

        if (_input != null)
        {
            var playerInput = new Controls();
            playerInput.PlayerMap.SetCallbacks(_input);
            playerInput.PlayerMap.Enable();
        }
    }

    public override void OnMove()
    {
        Vector2 move = _input.MoveInput.normalized;
        RigidbodyCompo.velocity = _moveSpeed * move;
        // 이동속도와 deltaTime에 방향을 곱해 이동

        Flip();
    }

    public override void Flip()
    {
        if (_input.MoveInput.x != 0)
        {
            _lastDirection = (int)Mathf.Sign(_input.MoveInput.x);
        }

        transform.localScale = new Vector3(_lastDirection, 1, 1);
    }
}
