using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [HideInInspector]
    public AudioSource AudioSource;

    public KeyCode InteractKey = KeyCode.E;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start() {
        AudioSource = GetComponent<AudioSource>();
    }

    public static void ResetSwitches() {
        foreach(Switch switchObj in FindObjectsOfType<Switch>()) {
            switchObj.ResetValue();
        }
    }
}
