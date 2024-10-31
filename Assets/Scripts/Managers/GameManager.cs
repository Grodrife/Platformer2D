using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager s_gameManager;
    private GameSceneManager _gameSceneManager;
    private bool _isGamePaused;
    private string _spawnPointString;
    public int currentResolutionDropdownIndex = 2;
    public bool currentFullscreenStatus = true;

    private void Awake()
    {
        if (s_gameManager == null)
        {
            s_gameManager = this;
            DontDestroyOnLoad(gameObject);
        } else if (s_gameManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        EventsHandler.GameEvents.OnReturnMainMenu += ReturnMainMenu;
        EventsHandler.GameEvents.OnChangeGamePause += ManagePauseGame;
        EventsHandler.GameEvents.OnResetLevel += ResetLevel;
        EventsHandler.GameEvents.OnCloseGame += CloseGame;
        EventsHandler.GameEvents.OnResetGame += LoadGame;
        EventsHandler.PlayerEvents.OnPlayerDies += PlayerDies;
        EventsHandler.WorldElementsEvents.OnPlayerEntersExitPoint += ChangeScene;
        EventsHandler.UIEvents.OnResolutionChanged += ChangeGameResolution;
        EventsHandler.UIEvents.OnFullscreenChanged += ChangeFullscreen;
    }
    void Start()
    {
        EventsHandler.UIEvents.OnHideHUDHandler();
        EventsHandler.UIEvents.OnHidePauseMenuHandler();
        EventsHandler.UIEvents.OnShowMainMenuHandler();
        EventsHandler.InputsEvents.OnDeactivatePlayerInputHandler();
        EventsHandler.InputsEvents.OnActivateUIInputHandler();
        _gameSceneManager = GameSceneManager.s_gameSceneManager;
        _spawnPointString = "WestSpawnPoint";
    }
    public void ManagePauseGame()
    {
        if (_isGamePaused)
        {
            ResumeGame();
        } else
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        EventsHandler.AudioEvents.OnPauseBackgroundAudioHandler();
        EventsHandler.InputsEvents.OnDeactivatePlayerInputHandler();
        EventsHandler.InputsEvents.OnActivateUIInputHandler();
        Time.timeScale = 0;
        _isGamePaused = true;
        EventsHandler.GameEvents.OnManageGamePauseHandler(true);
        EventsHandler.UIEvents.OnHideHUDHandler();
        EventsHandler.UIEvents.OnShowPauseMenuHandler();
    }
    private void ResumeGame()
    {
        EventsHandler.UIEvents.OnHidePauseMenuHandler();
        EventsHandler.UIEvents.OnShowHUDHandler();
        EventsHandler.GameEvents.OnManageGamePauseHandler(false);
        _isGamePaused = false;
        Time.timeScale = 1;
        EventsHandler.InputsEvents.OnDeactivateUIInputHandler();
        EventsHandler.InputsEvents.OnActivatePlayerInputHandler();
        EventsHandler.AudioEvents.OnPlayBackgroundAudioHandler();
    }
    private void ResetLevel()
    {
        _gameSceneManager.ResetCurrentScene().completed += (AsyncOperation operation) =>
        {
            EventsHandler.GameEvents.OnSetPlayerPositionHandler(GetSpawnPosition(_spawnPointString));
            EventsHandler.GameEvents.OnSceneTotallyLoadedHandler();
        };
    }
    private void ChangeScene(string sceneToLoad, string spawnPointString)
    {
        _spawnPointString = spawnPointString;
        _gameSceneManager.ChangeScene(sceneToLoad).completed += (AsyncOperation operation) =>
        {
            EventsHandler.GameEvents.OnSetPlayerPositionHandler(GetSpawnPosition(_spawnPointString));
            EventsHandler.GameEvents.OnSceneTotallyLoadedHandler();
        };
    }
    public void LoadGame()
    {
        EventsHandler.UIEvents.OnHideMainMenuHandler();
        EventsHandler.UIEvents.OnHidePauseMenuHandler();
        EventsHandler.InputsEvents.OnDeactivateUIInputHandler();
        EventsHandler.InputsEvents.OnActivatePlayerInputHandler();
        _gameSceneManager.LoadGame().completed += (AsyncOperation operation) =>
        {
            _isGamePaused = false;
            Time.timeScale = 1;
            EventsHandler.PlayerEvents.OnInitializePlayerHandler();
            EventsHandler.UIEvents.OnShowHUDHandler();
            EventsHandler.AudioEvents.OnPlayBackgroundAudioHandler();
        };
    }
    public void ReturnMainMenu()
    {
        EventsHandler.AudioEvents.OnPauseBackgroundAudioHandler();
        EventsHandler.UIEvents.OnHidePauseMenuHandler();
        EventsHandler.UIEvents.OnHideHUDHandler();
        EventsHandler.InputsEvents.OnDeactivatePlayerInputHandler();
        EventsHandler.InputsEvents.OnActivateUIInputHandler();
        _gameSceneManager.ReturnMainMenu().completed += (AsyncOperation operation) =>
        {
            EventsHandler.UIEvents.OnShowMainMenuHandler();
        };
    }
    private Vector3 GetSpawnPosition(string spawnPointString)
    {
        return GameObject.Find(spawnPointString).transform.position;
    }
    private void PlayerDies()
    {
        Time.timeScale = 0;
        _isGamePaused = true;
    }
    private void CloseGame()
    {
        Application.Quit();
    }
    private void ChangeGameResolution(int choiceIndex)
    {
        currentResolutionDropdownIndex = choiceIndex;
        switch (choiceIndex)
        {
            case 0:
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            default:
                break;
        }
    }
    private void ChangeFullscreen(bool isFullscreen)
    {
        currentFullscreenStatus = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}
