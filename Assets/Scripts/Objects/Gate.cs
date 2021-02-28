using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gate : MonoBehaviour {
    public GateInputPort[] inputPorts;
    public SignalReceiver[] outputs;

    private void Start() {
        foreach(GateInputPort inputPort in inputPorts) {
            inputPort.SetGate(this);
        }
    }

    public void OnInputChanged() {
        bool result = ComputeGate(inputPorts);

        foreach(SignalReceiver receiver in outputs) {
            receiver.OnSignalReceived(result);
        }
    }

    protected abstract bool ComputeGate(GateInputPort[] inputPorts);
}
