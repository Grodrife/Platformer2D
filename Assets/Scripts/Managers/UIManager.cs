using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager s_UIManager;
    private GameManager _gameManager;
    public PauseMenu pauseMenu;
    public HUD hud;
    public MainMenu mainMenu;
    
    private void Awake()
    {
        if (s_UIManager == null)
        {
            s_UIManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (s_UIManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        InitializeManager();
    }
    void Start()
    {
        _gameManager = GameManager.s_gameManager;
        
    }
    private void InitializeManager()
    {
        pauseMenu = GetComponentInChildren<PauseMenu>();
        mainMenu = GetComponentInChildren<MainMenu>();
        hud = GetComponentInChildren<HUD>();
        EventsHandler.GameEvents.OnEnterGame += StartGame;
    }
    private void StartGame()
    {
        _gameManager.LoadGame();
        EventsHandler.GameEvents.OnReturnMainMenu += ReturnMainMenu;
    }
    private void ReturnMainMenu()
    {
        _gameManager.ReturnMainMenu();
    }
}
