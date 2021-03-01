using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : SignalReceiver {
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    [SerializeField]
    private DoorState doorState;

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
                //TODO: Next level
                Debug.Log("NEXT LEVEL!");
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
