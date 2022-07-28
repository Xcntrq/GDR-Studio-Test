using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _validationTreshold;
    [SerializeField] private float _speed;

    private ITargetPositionGenerator _targetPositionGenerator;
    private Queue<Vector2> _targetPositions;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _targetPositions = new Queue<Vector2>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (TryGetComponent(out ITargetPositionGenerator targetPositionGenerator))
        {
            _targetPositionGenerator = targetPositionGenerator;
        }
        else
        {
            Debug.LogWarning("PlayerMovement component couldn't find ITargetPositionGenerator");
            _targetPositionGenerator = new NullTargetPositionGenerator();
        }

        _targetPositionGenerator.OnTargetPositionGenerated += TargetPositionGenerator_OnTargetPositionGenerated;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = new(transform.position.x, transform.position.y);
        Vector2 currentTargetPos;

        // Dequeue ALL potentially reached positions
        bool isTargetAway = false;
        while ((_targetPositions.Count > 0) && (!isTargetAway))
        {
            currentTargetPos = _targetPositions.Peek();

            if (Vector2.Distance(currentTargetPos, currentPosition) <= _validationTreshold)
                _targetPositions.Dequeue();
            else
                isTargetAway = true;
        }

        // Move to the next distant target
        if (_targetPositions.Count > 0)
        {
            currentTargetPos = _targetPositions.Peek();
            Vector2 direction = currentTargetPos - currentPosition;
            _rigidbody2D.MovePosition(_rigidbody2D.position + (_speed * Time.deltaTime * direction.normalized));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            collectable.GetCollected();
        }
    }

    private void OnDestroy()
    {
        _targetPositionGenerator.OnTargetPositionGenerated -= TargetPositionGenerator_OnTargetPositionGenerated;
    }

    private void TargetPositionGenerator_OnTargetPositionGenerated(Vector2 newTargetPosition)
    {
        _targetPositions.Enqueue(newTargetPosition);
    }
}
