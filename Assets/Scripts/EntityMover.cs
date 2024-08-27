using System.Collections.Generic;
using UnityEngine;

public class EntityMover
{
    private float _moveSpeed;
    private float _collisionOffset = 0.05f;
    private ContactFilter2D _contactFilter;

    private Rigidbody2D _rb;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();

    public EntityMover(Rigidbody2D rigidbody, float moveSpeed = 1f)
    {
        _rb = rigidbody;
        _moveSpeed = moveSpeed;
    }

    public bool TryMove(Vector2 direction)
    {
        // Check for collisions
        int count = _rb.Cast(
            direction,
            _contactFilter,
            _castCollisions,
            _moveSpeed * Time.fixedDeltaTime + _collisionOffset
        );

        if (count == 0)
        {
            _rb.MovePosition(_rb.position + direction * _moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryMoveWithSliding(Vector2 direction)
    {
        bool success = TryMove(direction);

        // If there is an obstacle, this code checks if its still possible to move along either the x or y axis
        if (!success)
        {
            success = TryMove(new Vector2(direction.x, 0));
            if (!success)
            {
                success = TryMove(new Vector2(0, direction.y));
            }
        }

        return success;
    }
}
