using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour, PlayerInputSystem.IPlayerActions, PlayerInputSystem.IUIActions
{
    public static InputManager s_inputManager;
    private PlayerInputSystem _playerInputSystem;
    private UIManager _uiManager;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        InitializeActions();
        if (s_inputManager == null)
        {
            s_inputManager = this;
            DontDestroyOnLoad(gameObject);
        } else if (s_inputManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        _playerInputSystem.Enable();
        _playerInputSystem.UI.Enable();
    }
    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }
    private void InitializeActions()
    {
        _playerInputSystem.Player.SetCallbacks(this);
        _playerInputSystem.UI.SetCallbacks(this);
    }
    private void Start()
    {
        _uiManager = UIManager.s_UIManager;
    }
    void PlayerInputSystem.IPlayerActions.OnMovement(InputAction.CallbackContext context)
    {
        EventsHandler.InputsEvents.OnMovementInputsHandler(context);
    }
    void PlayerInputSystem.IPlayerActions.OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsHandler.InputsEvents.OnJumpInputHandler();
        }
    }
    void PlayerInputSystem.IPlayerActions.OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsHandler.InputsEvents.OnAttackInputHandler();
        }
    }
    void PlayerInputSystem.IPlayerActions.OnPauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsHandler.InputsEvents.OnPauseGameInputHandler();
        }
    }
    void PlayerInputSystem.IUIActions.OnNavigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsHandler.InputsEvents.OnNavigateInputsHandler();
        }
    }
    void PlayerInputSystem.IUIActions.OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsHandler.InputsEvents.OnSubmitInputHandler();
        }
    }
    void PlayerInputSystem.IUIActions.OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _uiManager.pauseMenu.OnBackButtonPressed();
            EventsHandler.InputsEvents.OnCancelInputHandler();
        }
    }
    public void OnClick(InputAction.CallbackContext context)
    {
    }
}
