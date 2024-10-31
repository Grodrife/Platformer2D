using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IHealthSystem
{
    private GameManager _gameManager;
    private PlayerController _playerController;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private PlayerStatsScriptableObject _playerStatsScriptableObject;
    private WorldEdge _worldEdge;
    private CinemachineVirtualCamera _camera;
    public float MaxHealth { get => _playerStatsScriptableObject.maxHealth; set => _playerStatsScriptableObject.maxHealth = value; }
    public float CurrentHealth { get => _playerStatsScriptableObject.currentHealth; set => _playerStatsScriptableObject.currentHealth = value; }
    private void OnEnable()
    {
        EventsHandler.InputsEvents.OnMovementInputs += OnMovementButtonsPressed;
        EventsHandler.InputsEvents.OnJumpInput += OnJumpButtonPressed;
        EventsHandler.InputsEvents.OnAttackInput += OnAttackButtonPressed;
        EventsHandler.InputsEvents.OnPauseGameInput += OnPauseButtonPressed;
        EventsHandler.InputsEvents.OnSubmitInput += OnSubmitButtonPressed;
        EventsHandler.GameEvents.OnSetPlayerPosition += SetPlayerPosition;
        EventsHandler.PlayerEvents.OnInitializePlayer += UpdateSceneObjectsReferences;
        EventsHandler.GameEvents.OnReturnMainMenu += DestroyPlayer;
    }

    private void OnDestroy()
    {
        EventsHandler.InputsEvents.OnMovementInputs -= OnMovementButtonsPressed;
        EventsHandler.InputsEvents.OnJumpInput -= OnJumpButtonPressed;
        EventsHandler.InputsEvents.OnAttackInput -= OnAttackButtonPressed;
        EventsHandler.InputsEvents.OnPauseGameInput -= OnPauseButtonPressed;
        EventsHandler.GameEvents.OnSetPlayerPosition -= SetPlayerPosition;
        EventsHandler.PlayerEvents.OnInitializePlayer -= UpdateSceneObjectsReferences;
        EventsHandler.GameEvents.OnReturnMainMenu -= DestroyPlayer;
    }
    void Start()
    {
        CurrentHealth = MaxHealth;
        EventsHandler.UIEvents.OnHealthBarValueChangedHandler(CurrentHealth);
        _gameManager = GameManager.s_gameManager;
        _playerController = GetComponent<PlayerController>();
        UpdateSceneObjectsReferences();
    }
    void Update()
    {
        _playerController.MoveCharacter(_horizontalInput);
        _playerController.ClimbLadders(_verticalInput);
    }
    public void OnMovementButtonsPressed(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x;
        _verticalInput = context.ReadValue<Vector2>().y;
    }
    public void OnJumpButtonPressed() { _playerController.Jump(); }
    public void OnAttackButtonPressed() { _playerController.Attack(); }
    public void OnPauseButtonPressed() { _gameManager.ManagePauseGame(); }
    public void OnBackButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FindAnyObjectByType<PauseMenu>().OnBackButtonPressed();
        }
    }
    public void OnSubmitButtonPressed(){}
    public void OnNavigationPressed(InputAction.CallbackContext context) {}
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        EventsHandler.UIEvents.OnHealthBarValueChangedHandler(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        EventsHandler.PlayerEvents.OnPlayerDiesHandler();
    }
    public void SetPlayerPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        UpdateSceneObjectsReferences();
    }
    private void UpdateSceneObjectsReferences()
    {
        _worldEdge = FindAnyObjectByType<WorldEdge>();
        if (_worldEdge != null)
        {
            EventsHandler.WorldElementsEvents.OnPlayerEntersWorldEdge += TakeDamage;
        }
        _camera = FindAnyObjectByType<CinemachineVirtualCamera>();
        _camera.Follow = transform;
    }
    private void DestroyPlayer(){ Destroy(gameObject); }
}
