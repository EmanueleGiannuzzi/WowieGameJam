using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CallOnContact : SignalReceiver {
    protected bool isActive = false;

    public override void OnSignalReceived(bool active) {
        isActive = active;
    }

    protected override void OnInteract() {}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(isActive && collision.CompareTag("Player")) {
            OnContact();
        }
    }

    protected abstract void OnContact();
}
