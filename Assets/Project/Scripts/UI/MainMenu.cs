using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    // PAUSE MENU
    public GameObject pauseMenu;

    public AudioMixer mixer;

    // QUALITY
    public TextMeshProUGUI qualityText;
    private int currentQuality;

    // BRIGHTNESS
    public CanvasGroup brightnessOverlay;

    // FULLSCREEN
    public Toggle fullscreenToggle;

    // TAB SWITCHING
    public GameObject audioPanel;
    public GameObject graphicsPanel;
    public GameObject controlsPanel;

    void Start()
    {
        ShowAudio();

        // Get current quality
        currentQuality = QualitySettings.GetQualityLevel();

        // Update text
        if (qualityText != null)
            qualityText.text = QualitySettings.names[currentQuality];

        // Fullscreen toggle sync
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;

        Debug.Log("Current Quality: " + QualitySettings.names[currentQuality]);
    }

    // START GAME
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    // OPEN OPTIONS
    public void OpenOptions()
    {
        Debug.Log("OPEN OPTIONS");

        // Main menu scene support
        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);

        // Pause menu support
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    // OPEN CREDITS
    public void OpenCredits()
    {
        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    // BACK BUTTON
    public void BackToMenu()
    {
        Debug.Log("BACK BUTTON");

        // Close options
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        // Main menu scene
        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);

        // Pause menu scene
        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        // Close credits if open
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

    // QUIT GAME
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    // TAB SWITCHING
    public void ShowAudio()
    {
        if (audioPanel != null)
            audioPanel.SetActive(true);

        if (graphicsPanel != null)
            graphicsPanel.SetActive(false);

        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    public void ShowGraphics()
    {
        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (graphicsPanel != null)
            graphicsPanel.SetActive(true);

        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (graphicsPanel != null)
            graphicsPanel.SetActive(false);

        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }

    // AUDIO SETTINGS
    public void SetMusicVolume(float value)
    {
        float volume = Mathf.Lerp(-80f, 0f, value);
        mixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float value)
    {
        float volume = Mathf.Lerp(-80f, 0f, value);
        mixer.SetFloat("SFXVolume", volume);
    }

    // QUALITY SETTINGS
    public void IncreaseQuality()
    {
        Debug.Log("INCREASE BUTTON CLICKED");

        currentQuality++;

        if (currentQuality >= QualitySettings.names.Length)
            currentQuality = 0;

        QualitySettings.SetQualityLevel(currentQuality, true);

        if (qualityText != null)
            qualityText.text = QualitySettings.names[currentQuality];

        Debug.Log("New Quality Index: " + currentQuality);
        Debug.Log("New Quality Name: " + QualitySettings.names[currentQuality]);
    }

    public void DecreaseQuality()
    {
        Debug.Log("DECREASE BUTTON CLICKED");

        currentQuality--;

        if (currentQuality < 0)
            currentQuality = QualitySettings.names.Length - 1;

        QualitySettings.SetQualityLevel(currentQuality, true);

        if (qualityText != null)
            qualityText.text = QualitySettings.names[currentQuality];

        Debug.Log("New Quality Index: " + currentQuality);
        Debug.Log("New Quality Name: " + QualitySettings.names[currentQuality]);
    }

    // BRIGHTNESS
    public void SetBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            brightnessOverlay.alpha = value;
        }
    }

    // FULLSCREEN
    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        Debug.Log("Fullscreen: " + isFullscreen);
    }
}