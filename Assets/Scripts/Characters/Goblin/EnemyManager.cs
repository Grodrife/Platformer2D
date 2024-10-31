using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IHealthSystem, IEnemyStats
{
    private StateController _stateController;
    private EnemyAnimator _enemyAnimator;
    [SerializeField] public GoblinStatsScriptableObject goblinStats;
    private float _maxHealth;
    private float _currentHealth;
    private float _attackDamage;
    private float _movementSpeed;
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }

    private void OnEnable()
    {
        UpdateReferences();
        EventsHandler.EnemyEvents.OnPlayerInAttackRange += EnterAttackState;
        EventsHandler.EnemyEvents.OnEnemyEndsAttacking += ExitAttackState;
        EventsHandler.GameEvents.OnSceneTotallyLoaded += UpdateReferences;
    }
    private void OnDestroy()
    {
        EventsHandler.EnemyEvents.OnPlayerInAttackRange -= EnterAttackState;
        EventsHandler.EnemyEvents.OnEnemyEndsAttacking -= ExitAttackState;
        EventsHandler.GameEvents.OnSceneTotallyLoaded -= UpdateReferences;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void EnterAttackState()
    {
        _enemyAnimator.StartAttackPlayer();
    }
    public void ExitAttackState()
    {
        _stateController.EndAttackPlayer();
    }
    private void UpdateReferences()
    {
        MaxHealth = goblinStats.maxHealth;
        CurrentHealth = MaxHealth;
        AttackDamage = goblinStats.attackDamage;
        MovementSpeed = goblinStats.movementSpeed;
        _stateController = GetComponentInChildren<StateController>();
        _stateController.InitializeController(this);
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }
}
