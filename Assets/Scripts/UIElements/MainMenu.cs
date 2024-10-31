using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private UIDocument _UIDocument;
    private VisualElement _rootVisualElement;
    private VisualElement _buttonsContainer;
    private Button _playButton;
    private Button _optionsButton;
    private Button _exitButton;
    private VisualElement _optionsContainer;
    private Slider _volumeSlider;
    private Toggle _fullscreenToggle;
    private DropdownField _resolutionDropdown;
    private VisualElement _exitGameContainer;
    private Label _confirmLabel;
    private Button _confirmExitButton;
    private Button _cancelExitButton;
    private void OnEnable()
    {
        InitializeMenu();
        EventsHandler.UIEvents.OnShowMainMenu += ShowMainMenu;
        EventsHandler.UIEvents.OnHideMainMenu += HideMainMenu;
    }
    void Start()
    {
        _gameManager = GameManager.s_gameManager;
        _audioManager = AudioManager.s_audioManager;
    }
    private void ShowMainMenu() { _rootVisualElement.style.display = DisplayStyle.Flex; }
    private void HideMainMenu() { _rootVisualElement.style.display = DisplayStyle.None; }
    private void InitializeMenu()
    {
        _UIDocument = GetComponentInChildren<UIDocument>();
        _rootVisualElement = _UIDocument.rootVisualElement;
        _buttonsContainer = _rootVisualElement.Q("ButtonsContainer");
        _optionsContainer = _rootVisualElement.Q("OptionsContainer");
        SetButtons();
        SetOptionsContainer();
        SetExitGameContainer();
        _playButton.Focus();
        EventsHandler.InputsEvents.OnCancelInput += OnBackButtonPressed;
    }
    private void SetExitGameContainer()
    {
        _exitGameContainer = _rootVisualElement.Q("ExitGameContainer");
        _confirmLabel = _exitGameContainer.Q("ConfirmationLabel") as Label;
        _confirmExitButton = _exitGameContainer.Q("ConfirmExitButton") as Button;
        _confirmExitButton.text = "Yes";
        _cancelExitButton = _exitGameContainer.Q("CancelExitButton") as Button;
        _cancelExitButton.text = "Cancel";
        _confirmExitButton.clicked += OnConfirmExitButtonClicked;
        _confirmExitButton.clicked += EventsHandler.AudioEvents.OnPlayHoverAudioHandler;
        _cancelExitButton.clicked += OnCancelExitButtonClicked;
        _cancelExitButton.clicked += EventsHandler.AudioEvents.OnPlayHoverAudioHandler;
        _confirmLabel.text = "Exit Game ?";
        _optionsContainer.style.display = DisplayStyle.None;
        _exitGameContainer.style.display = DisplayStyle.None;
    }
    private void SetButtons()
    {
        _playButton = _rootVisualElement.Q("PlayButton") as Button;
        _optionsButton = _rootVisualElement.Q("OptionsButton") as Button;
        _exitButton = _rootVisualElement.Q("CloseGameButton") as Button;
        _playButton.text = "Play";
        _optionsButton.text = "Options";
        _exitButton.text = "Exit";
        _playButton.clicked += OnPlayButtonClicked;
        _playButton.clicked += EventsHandler.AudioEvents.OnPlayHoverAudioHandler;
        _optionsButton.clicked += OnOptionsButtonClicked;
        _optionsButton.clicked += EventsHandler.AudioEvents.OnPlayHoverAudioHandler;
        _exitButton.clicked += OnExitButtonClicked;
        _exitButton.clicked += EventsHandler.AudioEvents.OnPlayHoverAudioHandler;
    }
    private void SetOptionsContainer()
    {
        _volumeSlider = _optionsContainer.Q("VolumeSlider") as Slider;
        _fullscreenToggle = _optionsContainer.Q("FullscreenToggle") as Toggle;
        _resolutionDropdown = _optionsContainer.Q("ResolutionDropdown") as DropdownField;
        _volumeSlider.Q<Label>().text = "Volume";
        _volumeSlider.RegisterValueChangedCallback(value =>
        {
            EventsHandler.AudioEvents.OnVolumeSliderChangedHandler(value.newValue);
        });
        _fullscreenToggle.Q<Label>().text = "Fullscreen";
        _fullscreenToggle.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            EventsHandler.UIEvents.OnFullscreenChangeHandler(evt.newValue);
        });
        _resolutionDropdown.Q<Label>().text = "Resolution";
        _resolutionDropdown.choices = new List<string> { "3840x2160", "2560x1440", "1920x1080", "1280x720"};
        _resolutionDropdown.index = 2;
        _resolutionDropdown.RegisterValueChangedCallback(_ => EventsHandler.UIEvents.OnResolutionChangedHandler(_resolutionDropdown.index));
    }
    private void OnPlayButtonClicked()
    {
        Destroy(FindAnyObjectByType<PlayerInput>().gameObject);
        EventsHandler.GameEvents.OnEnterGameHandler();
    }
    private void OnOptionsButtonClicked()
    {
        _optionsContainer.style.display = DisplayStyle.Flex;
        UpdateOptionsContainer();
        UpdateMainButtonsFocus();
        _volumeSlider.Focus();
    }
    private void OnExitButtonClicked()
    {
        _exitGameContainer.style.display = DisplayStyle.Flex;
        UpdateMainButtonsFocus();
        _confirmExitButton.Focus();
    }
    private void OnConfirmExitButtonClicked() { EventsHandler.GameEvents.OnCloseGameHandler(); }
    private void OnCancelExitButtonClicked()
    {
        _exitGameContainer.style.display = DisplayStyle.None;
        UpdateMainButtonsFocus();
        _exitButton.Focus();
    }
    public void OnBackButtonPressed()
    {
        if (_optionsContainer.style.display == DisplayStyle.Flex)
        {
            CloseOptionsContainer();
        }

        if (_exitGameContainer.style.display == DisplayStyle.Flex)
        {
            CloseExitGameContainer();
        }
    }
    private void CloseOptionsContainer()
    {
        _optionsContainer.style.display = DisplayStyle.None;
        UpdateMainButtonsFocus();
        _optionsButton.Focus();
    }
    private void CloseExitGameContainer()
    {
        _exitGameContainer.style.display = DisplayStyle.None;
        UpdateMainButtonsFocus();
        _exitButton.Focus();
    }
    private void UpdateMainButtonsFocus()
    {
        _playButton.focusable = !_playButton.focusable;
        _playButton.pickingMode = _playButton.pickingMode == PickingMode.Ignore ? PickingMode.Position : PickingMode.Ignore;
        _optionsButton.focusable = !_optionsButton.focusable;
        _optionsButton.pickingMode = _optionsButton.pickingMode == PickingMode.Ignore ? PickingMode.Position : PickingMode.Ignore;
        _exitButton.focusable = !_exitButton.focusable;
        _exitButton.pickingMode = _exitButton.pickingMode == PickingMode.Ignore ? PickingMode.Position : PickingMode.Ignore;
    }
    private void UpdateOptionsContainer()
    {
        _volumeSlider.value = _audioManager.GetCurrentVolume();
        _resolutionDropdown.index = _gameManager.currentResolutionDropdownIndex;
        _fullscreenToggle.value = _gameManager.currentFullscreenStatus;
    }
}
