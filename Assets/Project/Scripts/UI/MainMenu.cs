using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsPanel;
    //public GameObject creditsPanel;
    public AudioMixer mixer;

    // QUALITY
    public TextMeshProUGUI qualityText;
    int qualityIndex;

    // BRIGHTNESS
    public CanvasGroup brightnessOverlay;

    // START GAME
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
        //creditsPanel.SetActive(true);
    }

    // BACK TO MAIN MENU
    public void BackToMenu()
    {
        mainMenuUI.SetActive(true);
        optionsPanel.SetActive(false);
        //creditsPanel.SetActive(false);
    }

    // QUIT GAME
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    // TAB SWITCHING

    public GameObject audioPanel;
    public GameObject graphicsPanel;
    public GameObject controlsPanel;

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

    void Start()
    {
        ShowAudio(); // default tab

        // Set current quality at start
        qualityIndex = QualitySettings.GetQualityLevel();
        UpdateQualityUI();
    }

    public void SetMusicVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        mixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        mixer.SetFloat("SFXVolume", volume);
    }


    // GRAPHICS SETTINGS
    // QUALITY
    public void IncreaseQuality()
    {
        qualityIndex++;
        if (qualityIndex >= QualitySettings.names.Length)
            qualityIndex = QualitySettings.names.Length - 1;

        ApplyQuality();
    }

    public void DecreaseQuality()
    {
        qualityIndex--;
        if (qualityIndex < 0)
            qualityIndex = 0;

        ApplyQuality();
    }

    void ApplyQuality()
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        UpdateQualityUI();
    }

    void UpdateQualityUI()
    {
        qualityText.text = QualitySettings.names[qualityIndex];
    }

    // BRIGHTNESS
    public void SetBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            brightnessOverlay.alpha = 1f - value;
        }
    }

    // FULLSCREEN
    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}