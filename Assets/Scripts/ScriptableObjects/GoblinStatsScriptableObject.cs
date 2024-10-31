using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoblinStats", menuName = "ScriptableObjects/GoblinStats")]
public class GoblinStatsScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float attackDamage;
    public float movementSpeed;
}
