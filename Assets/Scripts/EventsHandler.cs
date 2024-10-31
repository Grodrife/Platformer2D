using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EventsHandler
{
    public class InputsEvents
    {
        public static event Action<InputAction.CallbackContext> OnMovementInputs;
        public static event Action OnJumpInput;
        public static event Action OnAttackInput;
        public static event Action OnPauseGameInput;
        public static event Action OnNavigateInputs;
        public static event Action OnSubmitInput;
        public static event Action OnCancelInput;
        public static event Action OnActivatePlayerInput;
        public static event Action OnDeactivatePlayerInput;
        public static event Action OnActivateUIInput;
        public static event Action OnDeactivateUIInput;

        public static void OnMovementInputsHandler(InputAction.CallbackContext context) { OnMovementInputs?.Invoke(context); }
        public static void OnJumpInputHandler() { OnJumpInput?.Invoke(); }
        public static void OnAttackInputHandler() { OnAttackInput?.Invoke(); }
        public static void OnPauseGameInputHandler() { OnPauseGameInput?.Invoke(); }
        public static void OnNavigateInputsHandler() { OnNavigateInputs?.Invoke(); }
        public static void OnSubmitInputHandler() { OnSubmitInput?.Invoke(); }
        public static void OnCancelInputHandler() { OnCancelInput?.Invoke(); }
        public static void OnActivatePlayerInputHandler() { OnActivatePlayerInput?.Invoke(); }
        public static void OnDeactivatePlayerInputHandler() { OnDeactivatePlayerInput?.Invoke(); }
        public static void OnActivateUIInputHandler() { OnActivateUIInput?.Invoke(); }
        public static void OnDeactivateUIInputHandler() { OnDeactivateUIInput?.Invoke(); }
    }

    public class GameEvents
    {
        public static event Action<bool> OnManageGamePause;
        public static event Action OnChangeGamePause;
        public static event Action<Vector3> OnSetPlayerPosition;
        public static event Action OnSceneTotallyLoaded;
        public static event Action OnReturnMainMenu;
        public static event Action OnEnterGame;
        public static event Action OnCloseGame;
        public static event Action OnResetLevel;
        public static event Action OnResetGame;

        public static void OnManageGamePauseHandler(bool active) { OnManageGamePause?.Invoke(active); }
        public static void OnChangeGamePauseHandler() { OnChangeGamePause?.Invoke(); }
        public static void OnSetPlayerPositionHandler(Vector3 newPosition) {  OnSetPlayerPosition?.Invoke(newPosition);}
        public static void OnSceneTotallyLoadedHandler() { OnSceneTotallyLoaded?.Invoke(); }
        public static void OnReturnMainMenuHandler() { OnReturnMainMenu?.Invoke(); }
        public static void OnEnterGameHandler() { OnEnterGame?.Invoke(); }
        public static void OnCloseGameHandler() { OnCloseGame?.Invoke(); }
        public static void OnResetLevelHandler() { OnResetLevel?.Invoke(); }
        public static void OnResetGameHandler() { OnResetGame?.Invoke(); }
    }

    public class UIEvents
    {
        public static event Action OnShowMainMenu;
        public static event Action OnHideMainMenu;
        public static event Action OnShowPauseMenu;
        public static event Action OnHidePauseMenu;
        public static event Action OnShowHUD;
        public static event Action OnHideHUD;
        public static event Action<float> OnHealthBarValueChanged;
        public static event Action<int> OnResolutionChanged;
        public static event Action<bool> OnFullscreenChanged;

        public static void OnShowMainMenuHandler() { OnShowMainMenu?.Invoke(); }
        public static void OnHideMainMenuHandler() { OnHideMainMenu?.Invoke(); }
        public static void OnShowPauseMenuHandler() { OnShowPauseMenu?.Invoke(); }
        public static void OnHidePauseMenuHandler() { OnHidePauseMenu?.Invoke(); }
        public static void OnShowHUDHandler() {  OnShowHUD?.Invoke(); }
        public static void OnHideHUDHandler() { OnHideHUD?.Invoke(); }
        public static void OnHealthBarValueChangedHandler(float newValue) { OnHealthBarValueChanged?.Invoke(newValue); }
        public static void OnResolutionChangedHandler(int choiceIndex) {  OnResolutionChanged?.Invoke(choiceIndex); }
        public static void OnFullscreenChangeHandler(bool isActive) {  OnFullscreenChanged?.Invoke(isActive); }
    }

    public class AudioEvents
    {
        public static event Action OnPlayHoverAudio;
        public static event Action OnPlayBackgroundAudio;
        public static event Action OnPauseBackgroundAudio;
        public static event Action<float> OnVolumeSliderChanged;
        
        public static void OnPlayHoverAudioHandler() { OnPlayHoverAudio?.Invoke(); }
        public static void OnPlayBackgroundAudioHandler() { OnPlayBackgroundAudio?.Invoke(); }
        public static void OnPauseBackgroundAudioHandler() { OnPauseBackgroundAudio?.Invoke(); }
        public static void OnVolumeSliderChangedHandler(float value) { OnVolumeSliderChanged?.Invoke(value); }
    }
    
    public class PlayerEvents
    {
        public static event Action OnPlayerChangesDirection;
        public static event Action OnPlayerAttacks;
        public static event Action OnPlayerEndsAttacking;
        public static event Action OnPlayerTakesDamage;
        public static event Action OnPlayerDies;
        public static event Action OnPlayerGrounded;
        public static event Action OnPlayerNotGrounded;
        public static event Action OnInitializePlayer;

        public static void OnPlayerChangesDirectionHandler() { OnPlayerChangesDirection?.Invoke(); }
        public static void OnPlayerAttacksHandler() {  OnPlayerAttacks?.Invoke(); }
        public static void OnPlayerEndsAttackingHandler() { OnPlayerEndsAttacking?.Invoke(); }
        public static void OnPlayerTakesDamageHandler() { OnPlayerTakesDamage?.Invoke(); }
        public static void OnPlayerDiesHandler() { OnPlayerDies?.Invoke(); }
        public static void OnPlayerGroundedHandler() {  OnPlayerGrounded?.Invoke(); }
        public static void OnPlayerNotGroundedHandler() { OnPlayerNotGrounded?.Invoke(); }
        public static void OnInitializePlayerHandler() {  OnInitializePlayer?.Invoke(); }
    }

    public class EnemyEvents
    {
        public static event Action OnEnemyEndsAttacking;
        public static event Action OnEnemyNearsEdge;
        public static event Action OnEnemyNearsWall;
        public static event Action OnPlayerInChaseRange;
        public static event Action OnPlayerNotInChaseRange;
        public static event Action OnPlayerInAttackRange;

        public static void OnEnemyEndsAttackingHandler() { OnEnemyEndsAttacking?.Invoke(); }
        public static void OnEnemyNearsEdgeHandler() { OnEnemyNearsEdge?.Invoke(); }
        public static void OnEnemyNearsWallHandler() { OnEnemyNearsWall?.Invoke(); }
        public static void OnPlayerInChaseRangeHandler() {  OnPlayerInChaseRange?.Invoke(); }
        public static void OnPlayerNotInChaseRangeHandler() { OnPlayerNotInChaseRange?.Invoke(); }
        public static void OnPlayerInAttackRangeHandler() { OnPlayerInAttackRange?.Invoke(); }
    }

    public class WorldElementsEvents
    {
        public static event Action<string, string> OnPlayerEntersExitPoint;
        public static event Action<float> OnPlayerEntersWorldEdge;

        public static void OnPlayerEntersExitPointHandler(string sceneToLoad, string spawnPointString) { OnPlayerEntersExitPoint?.Invoke(sceneToLoad, spawnPointString); }
        public static void OnPlayerEntersWorldEdgeHandler(float damage) { OnPlayerEntersWorldEdge?.Invoke(damage); }
    }
}
