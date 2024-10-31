using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour, IAttackSystem
{
    private Collider2D _collider;
    private Vector2 _colliderOffset;
    private PlayerController _playerController;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _colliderOffset = _collider.offset;
        EventsHandler.PlayerEvents.OnPlayerChangesDirection += ChangeColliderOffset;
    }
    private void OnDestroy()
    {
        EventsHandler.PlayerEvents.OnPlayerChangesDirection -= ChangeColliderOffset;
    }
    private void ChangeColliderOffset()
    {
        _collider.offset = new Vector2(_colliderOffset.x * -1, _colliderOffset.y);
        _colliderOffset = _collider.offset;
    }
    public void DealDamage(IHealthSystem targetHealth)
    {
        targetHealth.TakeDamage(_playerController._playerStats.attackDamage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealthSystem targetHealth = collision.GetComponent<IHealthSystem>();
        
        if (targetHealth != null)
        {
            DealDamage(targetHealth);
        }
    }
}
