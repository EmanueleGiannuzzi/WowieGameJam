using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : SignalReceiver {
    SpriteRenderer spriteRenderer;

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
    }

    protected override void OnInteract() {
        if(doorState == DoorState.UNLOCKED) {
            SetState(DoorState.OPEN);
        }
        else if(doorState == DoorState.OPEN) {
            //TODO: Next level
            Debug.Log("NEXT LEVEL!");
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
