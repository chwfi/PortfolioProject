using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MoveComponent
{
    [Header("InputReader")]
    [SerializeField] private InputReader _input;

    public InputReader Input => _input;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        if (_input != null)
        {
            var playerInput = new Controls();
            playerInput.PlayerMap.SetCallbacks(_input);
            playerInput.PlayerMap.Enable();
        }
    }

    public override void OnUpdate()
    {
        Vector2 move = _input.MoveInput.normalized;
        CharacterControllerCompo.Move(_moveSpeed * Time.deltaTime * move);
    }
}
