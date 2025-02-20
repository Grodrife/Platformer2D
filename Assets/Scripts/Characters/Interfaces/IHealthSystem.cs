using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSystem
{
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

    void TakeDamage(float damage);
    void Die();
}
