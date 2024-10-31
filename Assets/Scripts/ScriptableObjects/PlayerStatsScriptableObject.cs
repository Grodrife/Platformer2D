using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public float attackDamage;
    public float movementSpeed;
    public float jumpForce;
}
