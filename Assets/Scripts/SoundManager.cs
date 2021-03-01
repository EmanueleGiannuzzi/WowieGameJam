using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [Header("General")]
    public AudioMixer MainMixer;

    private const string MASTER_VOLUME = "MasterVolume";

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if(Instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    public float GetMasterVolume() {
         MainMixer.GetFloat(MASTER_VOLUME, out float volume);
        return volume;
    }

    public void SetMasterVolume(float volume) {
        MainMixer.SetFloat(MASTER_VOLUME, volume);
    }
}
