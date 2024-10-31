using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator s_playerAnimator;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
        if (s_playerAnimator == null)
        {
            s_playerAnimator = this;
            DontDestroyOnLoad(gameObject);
        } else if (s_playerAnimator != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        EventsHandler.PlayerEvents.OnPlayerAttacks += CharacterAttack;
    }

    private void OnDestroy()
    {
        EventsHandler.PlayerEvents.OnPlayerAttacks -= CharacterAttack;
    }
    void Update()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) >= 0.8)
        {
            _animator.SetBool("Walking", true);
        } else
        {
            _animator.SetBool("Walking", false);
        }

        if (_playerController.isJumping)
        {
            if (_rigidbody.velocity.y >= 0)
            {
                _animator.SetBool("Jumping", true);
            } else
            {
                _animator.SetBool("Falling", true);
            }
        }
        else
        {
            _animator.SetBool("Jumping", false);
            _animator.SetBool("Falling", false);
        }

        if (_playerController.isOnLadder)
        {
            _animator.SetBool("OnLadder", true);
        } else
        {
            _animator.SetBool("OnLadder", false);
        }
    }
    private void CharacterAttack()
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
        EventsHandler.PlayerEvents.OnPlayerEndsAttackingHandler();
    }
}
