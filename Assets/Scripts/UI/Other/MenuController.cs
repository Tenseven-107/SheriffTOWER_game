using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider != null) volumeSlider.value = AudioListener.volume;
        if (fullscreenToggle != null) fullscreenToggle.isOn = Screen.fullScreen;
    }


    public void SwitchSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMusic()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void SetFullscreen()
    {
        if (Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
        }
        else { Screen.fullScreen = true;}
    }
}
