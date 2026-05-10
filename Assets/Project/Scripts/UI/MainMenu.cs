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

    public AudioMixer mixer;

    // QUALITY
    public TextMeshProUGUI qualityText;
    private int currentQuality = 0;

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
        ShowAudio(); // default tab

        // Set current quality text at start
        qualityText.text = QualitySettings.names[currentQuality];

        // Sync fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    // START GAME
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    // OPEN OPTIONS
    public void OpenOptions()
    {
        mainMenuUI.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // OPEN CREDITS
    public void OpenCredits()
    {
        mainMenuUI.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // BACK TO MAIN MENU
    public void BackToMenu()
    {
        mainMenuUI.SetActive(true);
        optionsPanel.SetActive(false);
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
        audioPanel.SetActive(true);
        graphicsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ShowGraphics()
    {
        audioPanel.SetActive(false);
        graphicsPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        audioPanel.SetActive(false);
        graphicsPanel.SetActive(false);
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

    // GRAPHICS SETTINGS

    // QUALITY
    public void IncreaseQuality()
    {
        currentQuality++;

        if (currentQuality >= QualitySettings.names.Length)
            currentQuality = 0;

        qualityText.text = QualitySettings.names[currentQuality];

        Debug.Log(qualityText.text);
    }

    public void DecreaseQuality()
    {
        currentQuality--;

        if (currentQuality < 0)
            currentQuality = QualitySettings.names.Length - 1;

        qualityText.text = QualitySettings.names[currentQuality];

        Debug.Log(qualityText.text);
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