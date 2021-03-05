using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : SignalReceiver {
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    [SerializeField] private DoorState doorState;
    //[SerializeField] private string nextSceneName;

    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite openSprite;

    public enum DoorState {
        LOCKED,
        UNLOCKED,
        OPEN
    }

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnInteract() {
        if(PlayerManager.Instance.IsAlive) {
            if(doorState == DoorState.UNLOCKED) {
                SetState(DoorState.OPEN);
                audioSource.Play();
            }
            else if(doorState == DoorState.OPEN) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void SetState(DoorState _doorState) {
        doorState = _doorState;

        spriteRenderer.sprite = _doorState == DoorState.OPEN ? openSprite : _doorState == DoorState.UNLOCKED ? unlockedSprite : lockedSprite;
    }

    public override void OnSignalReceived(bool active) {
        SetState(active ? DoorState.UNLOCKED : DoorState.LOCKED);
    }

}
