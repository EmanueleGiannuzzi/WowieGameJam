using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GateInputPort : SignalReceiver {
    SpriteRenderer spriteRenderer;

    public bool IsActive { private set; get; } = false;

    Gate gate;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canInteract = false;
    }

    public override void OnSignalReceived(bool active) {
        IsActive = active;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = IsActive ? Color.green : Color.red;

        gate.OnInputChanged();
    }

    public void SetGate(Gate gate) {
        this.gate = gate;
    }

    protected override void OnInteract() { }
}
