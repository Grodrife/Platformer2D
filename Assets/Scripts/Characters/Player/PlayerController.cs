using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] public PlayerStatsScriptableObject _playerStats;
    public bool isJumping {  get; private set; }
    public bool isAttacking { get; private set; }
    public bool isOnLadder { get; private set; }
    public bool isFacingRight { get; private set; }
    private bool _isGamePaused;

    private void OnEnable()
    {
        EventsHandler.PlayerEvents.OnPlayerGrounded += SetFalseJumpingBool;
        EventsHandler.PlayerEvents.OnPlayerNotGrounded += SetTrueJumpingBool;
        EventsHandler.PlayerEvents.OnPlayerEndsAttacking += ResetIsAttacking;
        EventsHandler.GameEvents.OnManageGamePause += ManagePauseGame;
    }
    private void Start()
    {
        _isGamePaused = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        isOnLadder = false;
    }
    private void Update()
    {
        ChangeDirection();
    }
    public void MoveCharacter(float inputX)
    {
        float speedModifier = 1;
        Vector2 targetVelocity;

        if (!isJumping)
        {
            if (isAttacking)
            {
                speedModifier = 0.25f;
            }
            else
            {
                speedModifier = 1;
            }
        } else
        {
            speedModifier = 0.75f;
        }

        if (!_isGamePaused)
        {
            targetVelocity = new Vector2(inputX * speedModifier * _playerStats.movementSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = targetVelocity;
        }
    }
    public void ClimbLadders(float inputY)
    {
        if (isOnLadder && !_isGamePaused)
        {
            Vector2 targetVelocity = new Vector2(_rigidbody.velocity.x, inputY * 0.5f * _playerStats.movementSpeed);
            _rigidbody.velocity = targetVelocity;
        }
    }
    public void Jump()
    {
        if (!isJumping && !_isGamePaused)
        {
            _rigidbody.AddForce(new Vector2(0f, _playerStats.jumpForce));
        }
    }
    public void Attack()
    {
        if (!isJumping && !isAttacking && !_isGamePaused)
        {
            isAttacking = true;
            EventsHandler.PlayerEvents.OnPlayerAttacksHandler();
        }
    }
    private void ResetIsAttacking()
    {
        isAttacking = false;
    }
    private void SetTrueJumpingBool()
    {
        isJumping = true;
    }
    private void SetFalseJumpingBool()
    {
        isJumping = false;
    }
    private void ChangeDirection()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) / _rigidbody.velocity.x < 0)
        {
            if (!isFacingRight)
            {
                EventsHandler.PlayerEvents.OnPlayerChangesDirectionHandler();
                isFacingRight = true;
            }
        } else if (Mathf.Abs(_rigidbody.velocity.x) / _rigidbody.velocity.x > 0)
        {
            if (isFacingRight)
            {
                EventsHandler.PlayerEvents.OnPlayerChangesDirectionHandler();
                isFacingRight = false;
            }
        }
    }
    private void ManagePauseGame(bool pauseGame)
    {
        _isGamePaused = pauseGame;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladders"))
        {
            isOnLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladders"))
        {
            isOnLadder = false;
        }
    }
}
