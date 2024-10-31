using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) > 0)
        {
            _animator.SetBool("Walking", true);
        } else
        {
            _animator.SetBool("Walking", false);
        }
    }
    public void StartAttackPlayer()
    {
        _animator.SetBool("Attacking", true);
        StartCoroutine(WaitForAnimationToStart());
    }
    private IEnumerator WaitForAnimationToStart()
    {
        yield return new WaitForEndOfFrame();
        float _animationAttackDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("LaunchEndAttackingEvent", _animationAttackDuration);
    }
    private void LaunchEndAttackingEvent()
    {
        _animator.SetBool("Attacking", false);
        _enemyManager.ExitAttackState();
    }
}
