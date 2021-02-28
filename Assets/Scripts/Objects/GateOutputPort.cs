using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GateOutputPort : SignalReceiver {
    public bool IsActive { private set; get; } = false;
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canInteract = false;
    }

    public override void OnSignalReceived(bool active) {
        IsActive = active;
        spriteRenderer.color = IsActive ? Color.green : Color.red;
    }

    protected override void OnInteract() {}
}
