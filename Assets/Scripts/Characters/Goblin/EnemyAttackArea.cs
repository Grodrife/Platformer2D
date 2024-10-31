using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private void Start()
    {
        _enemyManager = GetComponentInParent<EnemyManager>();
    }
    public void DealDamage(IHealthSystem targetHealth)
    {
        targetHealth.TakeDamage(_enemyManager.goblinStats.attackDamage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealthSystem targetHealth = collision.GetComponent<IHealthSystem>();

        if (targetHealth != null && collision.CompareTag("Player"))
        {
            DealDamage(targetHealth);
        }
    }
}
