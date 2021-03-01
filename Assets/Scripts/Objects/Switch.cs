using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Switch : Interactable {
    [SerializeField]
    private bool active = false;

    public Sprite enabledSprite;
    public Sprite disabledSprite;

    public SignalReceiver[] signalReceivers;

    SpriteRenderer spriteRenderer;
    bool lastActive = false;
    private bool defaultValue;

    [Header("Audio")]
    public AudioClip switchON;
    public AudioClip switchOFF;


    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetActive(active);
        defaultValue = active;
    }

    private void FixedUpdate() {
        if(lastActive != active) {
            SetActive(active);
        }
    }

    void SetActive(bool _active) {
        active = lastActive = _active;

        spriteRenderer.sprite = active ? enabledSprite : disabledSprite;

        foreach(SignalReceiver receiver in signalReceivers) {
            receiver.OnSignalReceived(active);
        }
    }

    protected override void OnInteract() {
        SetActive(!active);
        GameManager.Instance.AudioSource.PlayOneShot(active ? switchON : switchOFF);
    }

    private void OnDrawGizmos() {
        Gizmos.color = active ? Color.green : Color.blue;
        foreach(SignalReceiver receiver in signalReceivers) {
            if(receiver != null) {
                Gizmos.DrawLine(this.transform.position, receiver.transform.position);
            }
        }
    }

    public void ResetValue() {
        SetActive(defaultValue);
    }
}
