using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotGate : Gate {
    protected override bool ComputeGate(GateInputPort[] inputPorts) {
        return !inputPorts[0].IsActive;
    }
}
