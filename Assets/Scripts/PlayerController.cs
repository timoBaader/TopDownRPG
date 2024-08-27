using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float CollisionOffset = 0.05f;
    public ContactFilter2D ContactFilter;

    private Vector2 _movementInput;
    private Rigidbody2D _rb;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    private Animator _animator;
    private EntityMover _mover;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mover = new EntityMover(_rb, ContactFilter, MoveSpeed);
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero)
        {
            bool success = _mover.TryMoveWithSliding(_movementInput);

            if (success)
            {
                _animator.SetBool("isMoving", success);
            }
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }

    void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }
}
