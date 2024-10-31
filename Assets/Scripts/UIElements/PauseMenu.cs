using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private UIDocument _uiDocument;
    private VisualElement _rootVisualElement;
    private Button _playButton;
    private Button _optionsButton;
    private Button _exitButton;
    private VisualElement _secondaryPanel;
    private VisualElement _optionsContainer;
    private VisualElement _confirmationContainer;
    private Slider _volumeSlider;
    private Toggle _fullscreenToggle;
    private DropdownField _resolutionDropdown;
    private void OnEnable()
    {
        InitializeMenu();
        EventsHandler.UIEvents.OnShowPauseMenu += ShowPauseMenu;
        EventsHandler.UIEvents.OnHidePauseMenu += HidePauseMenu;
    }
    void Start()
    {
        _gameManager = GameManager.s_gameManager;
        _audioManager = AudioManager.s_audioManager;
    }
    private void ShowPauseMenu() { _rootVisualElement.style.display = DisplayStyle.Flex; }
    private void HidePauseMenu()
    {
        _secondaryPanel.style.display = DisplayStyle.None;
        _rootVisualElement.style.display = DisplayStyle.None;
    }
    private void InitializeMenu()
    {
        _uiDocument = GetComponentInChildren<UIDocument>();
        _rootVisualElement = _uiDocument.rootVisualElement;
        _secondaryPanel = _rootVisualElement.Q("SecondaryPanel");
        SetMenuPanel();
        SetSecondaryPanel();
    }
    private void SetMenuPanel()
    {
        SetResumeButton();
        SetOptionsButton();
        SetExitButton();
    }
    private void SetResumeButton()
    {
        _playButton = _rootVisualElement.Q("PlayButton") as Button;
        _playButton.text = "Resume";
        _playButton.clicked += OnResumeButtonClicked;
    }
    private void SetOptionsButton()
    {
        _optionsButton = _rootVisualElement.Q("OptionsButton") as Button;
        _optionsButton.text = "Options";
        _optionsButton.clicked += OnOptionsButtonClicked;
    }
    private void SetExitButton()
    {
        _exitButton = _rootVisualElement.Q("ExitButton") as Button;
        _exitButton.text = "Exit";
        _exitButton.clicked += OnExitButtonClicked;
    }
    private void SetOptionsContainer()
    {
        _optionsContainer = _rootVisualElement.Q("OptionsContainer");
        _optionsContainer.Q<Label>("OptionsLabel").text = "Options";
        _volumeSlider = _optionsContainer.Q<Slider>("VolumeSlider");
        _volumeSlider.Q<Label>().text = "Volume";
        _volumeSlider.RegisterValueChangedCallback(value =>
        {
            EventsHandler.AudioEvents.OnVolumeSliderChangedHandler(value.newValue);
        });
        _fullscreenToggle = _optionsContainer.Q<Toggle>("FullscreenToggle");
        _fullscreenToggle.Q<Label>().text = "Fullscreen";
        _fullscreenToggle.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            EventsHandler.UIEvents.OnFullscreenChangeHandler(evt.newValue);
        });
        _resolutionDropdown = _optionsContainer.Q<DropdownField>("ResolutionDropdown");
        _resolutionDropdown.Q<Label>().text = "Resolution";
        _resolutionDropdown.choices = new List<string> { "3840x2160", "2560x1440", "1920x1080", "1280x720" };
        _resolutionDropdown.index = 2;
        _resolutionDropdown.RegisterValueChangedCallback(_ => EventsHandler.UIEvents.OnResolutionChangedHandler(_resolutionDropdown.index));
    }
    private void SetConfirmationContainer()
    {
        _confirmationContainer = _rootVisualElement.Q("ConfirmationContainer");
        _confirmationContainer.Q<Label>("ConfirmationLabel").text = "Exit Main Menu?";
        _confirmationContainer.Q<Button>("ConfirmationButton").text = "Confirm";
        _confirmationContainer.Q<Button>("CancelButton").text = "Cancel";
        _confirmationContainer.Q<Button>("ConfirmationButton").clicked += OnConfirmExitGameButtonPressed;
        _confirmationContainer.Q<Button>("CancelButton").clicked += OnBackButtonPressed;
    }
    private void SetSecondaryPanel()
    {
        SetOptionsContainer();
        SetConfirmationContainer();
        _secondaryPanel.style.display = DisplayStyle.None;
    }
    private void OnResumeButtonClicked()
    {
        EventsHandler.AudioEvents.OnPlayHoverAudioHandler();
        EventsHandler.GameEvents.OnChangeGamePauseHandler();
        EventsHandler.GameEvents.OnManageGamePauseHandler(false);
        EventsHandler.InputsEvents.OnDeactivateUIInputHandler();
        EventsHandler.InputsEvents.OnActivatePlayerInputHandler();
    }
    private void OnOptionsButtonClicked()
    {
        EventsHandler.AudioEvents.OnPlayHoverAudioHandler();
        UpdateOptionsContainer();
        _secondaryPanel.style.display = DisplayStyle.Flex;
        _confirmationContainer.style.display = DisplayStyle.None;
        _optionsContainer.style.display = DisplayStyle.Flex;
        _optionsContainer.Focus();
    }
    private void OnExitButtonClicked()
    {
        EventsHandler.AudioEvents.OnPlayHoverAudioHandler();      
        _secondaryPanel.style.display = DisplayStyle.Flex;
        _optionsContainer.style.display = DisplayStyle.None;
        _confirmationContainer.style.display = DisplayStyle.Flex;
        _playButton.focusable = false;
        _optionsButton.focusable = false;
        _exitButton.focusable = false;
        _confirmationContainer.Q("ConfirmationButton").Focus();
    }
    public void OnBackButtonPressed()
    {
        if (_optionsContainer.style.display == DisplayStyle.Flex || _confirmationContainer.style.display == DisplayStyle.Flex)
        {
            CloseSecondaryPanel();
        }
    }
    private void CloseSecondaryPanel()
    {
        _secondaryPanel.style.display = DisplayStyle.None;
        _playButton.focusable = true;
        _optionsButton.focusable = true;
        _exitButton.focusable = true;
        _playButton.Focus();
    }
    private void OnConfirmExitGameButtonPressed()
    {
        EventsHandler.AudioEvents.OnPlayHoverAudioHandler();
        EventsHandler.GameEvents.OnReturnMainMenuHandler();
    }
    private void UpdateOptionsContainer()
    {
        _volumeSlider.value = _audioManager.GetCurrentVolume();
        _resolutionDropdown.index = _gameManager.currentResolutionDropdownIndex;
        _fullscreenToggle.value = _gameManager.currentFullscreenStatus;
    }
}
