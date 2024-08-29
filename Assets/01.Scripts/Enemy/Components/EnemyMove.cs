using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveComponent
{
    private Entity _targetToMove => _owner.TargetCompo.GetTarget();

    private Vector2 _direction;

    public override void OnMove()
    {
        if (_targetToMove == null)
            return;
        
        _direction = ((Vector2)_targetToMove.transform.position - (Vector2)transform.position).normalized;

        RigidbodyCompo.velocity = _direction * _moveSpeed;

        Flip();
    }

    public override void Flip()
    {
        if (_direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
