using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SignalReceiver : Interactable {
    public abstract void OnSignalReceived(bool active);
}
