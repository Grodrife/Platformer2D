using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState {  get; private set; }
    public EnemyController enemyController { get; private set; }
    private EnemyManager _enemyManager;
    private PlayerManager _playerManager;
    private EnemyAnimator _enemyAnimator;
    public IdleState idleState { get; private set; }
    public WalkState walkState { get; private set; }
    public ChaseState chaseState { get; private set; }
    public AttackState attackState { get; private set; }
    private bool _isPlayerInRange;

    private void OnEnable()
    {
        UpdateReferences();
    }
    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.Do();
            if (currentState != attackState)
            {
                CheckDistanceToPlayer();
            }
        }
    }
    public void InitializeController(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        idleState = GetComponentInChildren<IdleState>();
        idleState.SetController(this);
        walkState = GetComponentInChildren<WalkState>();
        walkState.SetController(this);
        chaseState = GetComponentInChildren<ChaseState>();
        chaseState.SetController(this);
        attackState = GetComponentInChildren<AttackState>();
        attackState.SetController(this);
        ChangeState(idleState);
    }
    public void ChangeState(State nextState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = nextState;
        currentState.Enter();
    }
    private void StartChasePlayer()
    {
        _isPlayerInRange = true;
        ChangeState(chaseState);
    }
    private void StopChasePlayer()
    {
        _isPlayerInRange = false;
        ChangeState(idleState);
    }
    private void CheckDistanceToPlayer()
    {
        if (_playerManager == null)
        {
            _playerManager = FindAnyObjectByType<PlayerManager>();
        }
        if (!_isPlayerInRange)
        {
            if (Mathf.Abs(Vector2.Distance(transform.position, _playerManager.transform.position)) < 5)
            {
                StartChasePlayer();
            }
        } else
        {
            if (Mathf.Abs(Vector2.Distance(transform.position, _playerManager.transform.position)) > 5)
            {
                StopChasePlayer();
            } else if (Mathf.Abs(Vector2.Distance(transform.position, _playerManager.transform.position)) < 1)
            {
                AttackPlayer();
            }
        }
    }
    private void AttackPlayer()
    {
        ChangeState(attackState);
        _enemyManager.EnterAttackState();
    }
    public void EndAttackPlayer()
    {
        ChangeState(chaseState);
    }
    public void ChasePlayer()
    {
        enemyController.MoveToPosition(_playerManager.transform.position);
    }
    private void UpdateReferences()
    {
        _isPlayerInRange = false;
        _playerManager = FindAnyObjectByType<PlayerManager>();
        _enemyManager = GetComponentInParent<EnemyManager>();
        enemyController = GetComponentInParent<EnemyController>();
    }
}
