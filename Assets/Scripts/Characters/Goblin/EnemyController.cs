using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private WallChecker _wallChecker;
    [SerializeField] private EnemyGroundChecker _groundChecker;
    [SerializeField] private bool _isFacingRight;

    private void OnEnable()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _wallChecker = GetComponentInChildren<WallChecker>();
        _groundChecker = GetComponentInChildren<EnemyGroundChecker>();
        EventsHandler.EnemyEvents.OnEnemyNearsWall += FlipEnemy;
        EventsHandler.EnemyEvents.OnEnemyNearsEdge += FlipEnemy;
        _isFacingRight = true;
    }
    private void OnDestroy()
    {
        EventsHandler.EnemyEvents.OnEnemyNearsWall -= FlipEnemy;
        EventsHandler.EnemyEvents.OnEnemyNearsEdge -= FlipEnemy;
    }
    public void MoveEnemy()
    {
        Vector2 position = _isFacingRight ? Vector2.right : Vector2.left;
        ApplyMovement(position);
    }
    public void MoveToPosition(Vector2 position)
    {
        Vector2 directionToPosition = position - new Vector2(transform.position.x, transform.position.y);
        directionToPosition.y = 0;
        ApplyMovement(directionToPosition);
    }
    private void ApplyMovement(Vector2 position)
    {
        _rigidbody.velocity = new Vector2(position.normalized.x * _enemyManager.goblinStats.movementSpeed, _rigidbody.velocity.y);
        if (_rigidbody.velocity.x > 0 && !_isFacingRight)
        {
            FlipEnemy();
        }

        if (_rigidbody.velocity.x < 0 && _isFacingRight)
        {
            FlipEnemy();
        }
    }
    private void FlipEnemy()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        _isFacingRight = !_isFacingRight;
    }
}
