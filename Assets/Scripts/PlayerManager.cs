using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;

    public GameObject Player;
    public GameObject Ghost;

    public bool IsAlive { private set; get; } = true;

    private void Start() {
        Player.SetActive(true);
        Ghost.SetActive(false);
    }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void Die() {
        if(IsAlive) {
            IsAlive = false;

            Ghost.transform.position = Player.transform.position;

            Player.SetActive(false);
            Ghost.SetActive(true);
        }
    }
}
