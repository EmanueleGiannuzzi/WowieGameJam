using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    public Slider volumeSlider;

    private void Start() {
        volumeSlider.value = SoundManager.Instance.GetMasterVolume();
    }

    public void OnPlay() {
        SceneManager.LoadScene(1);
    }

    public void OnQuit() {
        Application.Quit();
    }

    public void OnVolumeSliderChanged(float value) {
        SoundManager.Instance.SetMasterVolume(value);
    }
}
