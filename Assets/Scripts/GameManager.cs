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

        Physics2D.IgnoreLayerCollision(12, 0, true);
        Physics2D.IgnoreLayerCollision(12, 1, true);
        Physics2D.IgnoreLayerCollision(12, 2, true);
        Physics2D.IgnoreLayerCollision(12, 4, true);
        Physics2D.IgnoreLayerCollision(12, 5, true);
        Physics2D.IgnoreLayerCollision(12, 8, true);
        Physics2D.IgnoreLayerCollision(12, 9, true);
        Physics2D.IgnoreLayerCollision(12, 10, true);
    }

    public static void ResetSwitches() {
        foreach(Switch switchObj in FindObjectsOfType<Switch>()) {
            switchObj.ResetValue();
        }
    }
}
