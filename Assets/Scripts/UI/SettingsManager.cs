using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings Data Containers")]
    [SerializeField]
    private GameSettings defaultSettings;
    [SerializeField]
    private GameSettings currentSettings;

    [Header("UI Elements")]
    [SerializeField]
    private AudioMixer mainMixer;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectsSlider;

    [SerializeField]
    private Button resetSettingsButton;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Toggle fullscreenToggle;

    private Resolution[] resolutions;

    void Start()
    {
        SetButtonsCallback();
        SetSettingsOnStart();
    }

    public void SetMusicVolume(Single volume)
    {
        mainMixer.SetFloat("Music", volume);
        currentSettings.musicVolume = volume;
    }

    public void SetEffectsVolume(Single volume)
    {
        mainMixer.SetFloat("Effects", volume);
        currentSettings.effectsVolume = volume;
    }

    public void SetMasterVolume(Single volume)
    {
        mainMixer.SetFloat("Master", volume);
        currentSettings.masterVolume = volume;
    }

    public void SetResolution(int index)
    {
        Resolution chosenResolution = resolutions[index];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, currentSettings.fullscreen);
        currentSettings.resolution = chosenResolution.ToString();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        currentSettings.fullscreen = isFullscreen;
    }

    private void ResetSettingsToDefault()
    {
        SetDefaulToCurrent();
        UpdateUI();
    }

    private void SetDefaulToCurrent()
    {
        currentSettings.masterVolume = defaultSettings.masterVolume;
        currentSettings.effectsVolume = defaultSettings.effectsVolume;
        currentSettings.musicVolume = defaultSettings.musicVolume;
        currentSettings.fullscreen = defaultSettings.fullscreen;
        currentSettings.resolution = defaultSettings.resolution;
    }

    private void UpdateUI()
    {
        masterSlider.value = currentSettings.masterVolume;
        effectsSlider.value = currentSettings.effectsVolume;
        musicSlider.value = currentSettings.musicVolume;
        fullscreenToggle.isOn = currentSettings.fullscreen;
        SetResolutionSettings();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].ToString() == defaultSettings.resolution)
            {
                resolutionDropdown.value = i;
            }
        }
    }

    private void CloseWindow()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetButtonsCallback()
    {
        resetSettingsButton.onClick.AddListener(ResetSettingsToDefault);
        closeButton?.onClick.AddListener(CloseWindow);
    }

    private void SetSettingsOnStart()
    {
        SetMixersOnStart();
        SetResolutionsOnStart();
        SetFullscreenOnStart();
        UpdateUI();
    }

    private void SetMixersOnStart()
    {
        mainMixer.SetFloat("Master", currentSettings.masterVolume);
        mainMixer.SetFloat("Music", currentSettings.musicVolume);
        mainMixer.SetFloat("Effects", currentSettings.effectsVolume);
    }

    private void SetResolutionsOnStart()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.options.Clear();
        if (currentSettings.resolution != "")
        {
            SetResolutionSettings();
        }
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        int index = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].ToString());
            if (resolutions[i].ToString() == Screen.currentResolution.ToString())
            {
                index = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        defaultSettings.resolution = resolutionDropdown.options[0].text;
        resolutionDropdown.value = index;
        currentSettings.resolution = resolutionDropdown.options[index].text;
    }

    private void SetFullscreenOnStart()
    {
        Screen.fullScreen = currentSettings.fullscreen;
        fullscreenToggle.isOn = currentSettings.fullscreen;
    }

    private void SetResolutionSettings()
    {
        String[] splitedResolution = currentSettings.resolution.Split(' ');
        int width = Int16.Parse(splitedResolution[0].Trim());
        int height = Int16.Parse(splitedResolution[2].Trim());
        Screen.SetResolution(width, height, currentSettings.fullscreen);
    }

    private void OnDestroy()
    {
        resetSettingsButton.onClick.RemoveAllListeners();
        closeButton?.onClick.RemoveAllListeners();
    }
}
