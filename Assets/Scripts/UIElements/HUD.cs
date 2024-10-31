using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _UIDocument;
    private VisualElement _rootElement;
    private VisualElement _healthBar;
    private VisualElement _controlsContainer;
    private VisualElement _endGameContainer;
    private Button _resetGameButton;
    private Button _exitGameButton;
    private void OnEnable()
    {
        InitializeHUD();
        EventsHandler.UIEvents.OnShowHUD += ShowHUD;
        EventsHandler.UIEvents.OnHideHUD += HideHUD;
        EventsHandler.UIEvents.OnHealthBarValueChanged += SetHealthPlayer;
        EventsHandler.PlayerEvents.OnPlayerDies += ShowEndGameContainer;
    }
    private void OnDestroy()
    {
        EventsHandler.UIEvents.OnShowHUD -= ShowHUD;
        EventsHandler.UIEvents.OnHideHUD -= HideHUD;
        EventsHandler.UIEvents.OnHealthBarValueChanged -= SetHealthPlayer;
        EventsHandler.PlayerEvents.OnPlayerDies -= ShowEndGameContainer;
    }
    private void InitializeHUD()
    {
        _UIDocument = GetComponentInChildren<UIDocument>();
        _rootElement = _UIDocument.rootVisualElement;
        _healthBar = _rootElement.Q("HealthBarValue");
        _controlsContainer = _rootElement.Q("ControlsContainer");
        _endGameContainer = _rootElement.Q("EndGameContainer");
        _resetGameButton = _rootElement.Q("ResetGameButton") as Button;
        _resetGameButton.clicked += OnResetGameButtonClicked;
        _exitGameButton = _rootElement.Q("ExitGameButton") as Button;
        _exitGameButton.clicked += OnExitGameButtonClicked;
    }
    private void ShowHUD()
    {
        _endGameContainer.style.display = DisplayStyle.None;
        _rootElement.style.display = DisplayStyle.Flex;
    }
    private void HideHUD() { _rootElement.style.display = DisplayStyle.None; }
    private void ShowEndGameContainer() { _endGameContainer.style.display = DisplayStyle.Flex; }
    private void SetHealthPlayer(float currentHealth)
    {
        Debug.Log(currentHealth);
        _healthBar.style.width = new StyleLength(new Length(currentHealth, LengthUnit.Percent));
    }
    private void OnResetGameButtonClicked() { EventsHandler.GameEvents.OnEnterGameHandler(); }
    private void OnExitGameButtonClicked() { EventsHandler.GameEvents.OnReturnMainMenuHandler(); }
}
