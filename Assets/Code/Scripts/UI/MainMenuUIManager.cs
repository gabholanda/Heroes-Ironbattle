using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public Button PlayButton, SettingsButton, QuitGame;
    public GameObject settingsCanvas;

    void Awake()
    {
        PlayButton.onClick.AddListener(Play);
        SettingsButton.onClick.AddListener(Settings);
        QuitGame.onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene("Main");
    }

    void Settings()
    {
        int children = settingsCanvas.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            settingsCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        PlayButton.onClick.RemoveAllListeners();
        SettingsButton.onClick.RemoveAllListeners();
        QuitGame.onClick.RemoveAllListeners();
    }
}